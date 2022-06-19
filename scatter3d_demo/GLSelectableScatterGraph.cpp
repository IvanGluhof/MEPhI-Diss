// GLSelectableScatterGraph.cpp : implementation file
//

#include "stdafx.h"
#include "GLSelectableScatterGraph.h"
#include <float.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/*
This class adds user selectability to the scattergraph.
To start making a selection call StartMakeSel().
After this call the user can draw any closed shape on the screen, and 
the program can detect which data points fall within the shape.
At the moment the only use made of this is ZoomSel, which sets the 
axes so as to zoom in on the selected points.
It would be easy to modify the class to carry out other functions on the
selected points.
*/
/////////////////////////////////////////////////////////////////////////////
// CGLSelectableScatterGraph

CGLSelectableScatterGraph::CGLSelectableScatterGraph()
{
	m_bMakeSel=FALSE;
	m_SelPts.SetSize(0,100);

//	test_num=0;
}

CGLSelectableScatterGraph::~CGLSelectableScatterGraph()
{
	m_SelPts.RemoveAll();
}


BEGIN_MESSAGE_MAP(CGLSelectableScatterGraph, CGLScatterGraph)
	//{{AFX_MSG_MAP(CGLSelectableScatterGraph)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// CGLSelectableScatterGraph message handlers
void CGLSelectableScatterGraph::StartMakeSel()
{
	m_bMakeSel=TRUE;
	m_bOldAllowRotate=m_bAllowMouseRotate;
	m_bAllowMouseRotate=FALSE;
}
void CGLSelectableScatterGraph::CancelSel()
{
	m_bMakeSel=FALSE;
	m_SelPts.RemoveAll();
	m_bAllowMouseRotate=m_bOldAllowRotate;
	Invalidate();
}

void CGLSelectableScatterGraph::OnLButtonDown(UINT nFlags, CPoint point) 
{
	if (m_bMakeSel)
	{
		if (m_SelPts.GetSize()!=0)
		{
			m_SelPts.RemoveAll();
//			m_SelList.RemoveAll();
			InvalidateRect(NULL);
		}
		m_SelPts.Add(point);
		m_PrevPt=point;
	}
	
	CGLScatterGraph::OnLButtonDown(nFlags, point);
}

void CGLSelectableScatterGraph::OnLButtonUp(UINT nFlags, CPoint point) 
{
	if (m_bMakeSel==TRUE && m_SelPts.GetSize()>0)
	{
		CClientDC dc( this );
		dc.SelectStockObject(WHITE_PEN);
		dc.MoveTo( m_PrevPt );
		dc.LineTo( point );
		dc.LineTo(m_SelPts.GetAt(0));
		m_SelPts.Add(point);

		m_bMakeSel=FALSE;

//		MakeSelList();
//		GetParent()->SendMessage(DONE_SELECTING);
	}
	
	CGLScatterGraph::OnLButtonUp(nFlags, point);
}

void CGLSelectableScatterGraph::OnMouseMove(UINT nFlags, CPoint point) 
{
// m_bMakeSel is set true by button click, and Capture is set
// in OnLButtonDown, which starts the drawing of the selection circle
	
	if (m_bMakeSel && m_SelPts.GetSize()>0)	// check whether had a mouse down
	{
		CClientDC dc( this );

		dc.SelectStockObject(WHITE_PEN);
		m_SelPts.Add(point);
		// Draw a line from the previous detected point in the mouse
		// drag to the current point.
		dc.MoveTo( m_PrevPt );
		dc.LineTo( point );
		m_PrevPt = point;
	}
	CGLScatterGraph::OnMouseMove(nFlags, point);
}

void CGLSelectableScatterGraph::OnDrawGDI(CPaintDC *pDC)
{
TRACE("In CGLSelectableScatterGraph::OnDrawGDI\n");	
	int i;
// draw the selection circle
	pDC->SelectStockObject(WHITE_PEN);
	int count=m_SelPts.GetSize();
	if (count>2)
	{
		pDC->MoveTo(m_SelPts.GetAt(0));
		for (i=1;i<count; i++)
			pDC->LineTo(m_SelPts.GetAt(i));
		pDC->LineTo(m_SelPts.GetAt(0));
	}

#if 0
	for (i=0; i<test_num; i++)
	{
		pDC->SetPixelV(test_coords[i][0],test_coords[i][1],RGB(255,255,255));
	}
#endif
}

BOOL CGLSelectableScatterGraph::ZoomSel()
{
TRACE("entering ZoomSel\n");
#ifdef _DEBUG
	int errNum;
#endif
	int i,j,count;
	int id=0;
	GLint rv;
	GLfloat token;
	GLfloat *pBuf;
#ifdef _DEBUG
		int hitCount=0;
#endif
	if (m_SelPts.GetSize()>2)
	{
// make a region from selPts
		CPoint *pt=new CPoint[m_SelPts.GetSize()];
		for (i=0; i<m_SelPts.GetSize(); i++)
			pt[i]=m_SelPts.GetAt(i);
		CRgn rgn;
		VERIFY(rgn.CreatePolygonRgn(pt,m_SelPts.GetSize(),ALTERNATE));
		delete [] pt;

// get a list of coordinate using feedback mode
// don't know how to calculate how much memory needed for buffer, but
// experiment shows the drawing has 96 floats overhead, + 4 per point
// give some spare, in case miscalculated !!
		pBuf=new GLfloat[200+m_Count*4];

		BeginGLCommands();
		glFeedbackBuffer(200+m_Count*4,GL_3D,pBuf);
		glRenderMode(GL_FEEDBACK);
#ifdef _DEBUG
		errNum=glGetError();
		TRACE("error on setting render mode = %s\n",gluErrorString(errNum));
		{
			int rvRenderMode;
			glGetIntegerv(GL_RENDER_MODE,&rvRenderMode);
			CString mode;
			if (rvRenderMode==GL_RENDER)
				mode="Render";
			else if (rvRenderMode==GL_FEEDBACK)
				mode="Feedback";
			else
				mode="Unknown";
		TRACE("Render mode = %s\n",mode);
		}
#endif
		EndGLCommands();
TRACE("ZoomSel just about to update window to store coords\n");
		Invalidate();
		UpdateWindow();

		BeginGLCommands();
		rv=glRenderMode(GL_RENDER);
TRACE("return val from setting RenderMode = %d\n",rv);

//////////
// now find which data points have which screen coords
		int *pInSel=new int[m_Count];	// for each pt, will be 1 if in, 0 if out

		GLint viewport[4];
//		GLdouble mvmatrix[16],projmatrix[16];
//		GLdouble wx,wy,wz;
		glGetIntegerv(GL_VIEWPORT,viewport);
//		glGetDoublev(GL_MODELVIEW_MATRIX,mvmatrix);
//		glGetDoublev(GL_PROJECTION_MATRIX,projmatrix);
		EndGLCommands();

		count=rv;
		while (count)
		{
			token=pBuf[rv-count];
			count--;
			if (token==GL_POINT_TOKEN)
			{
//				TRACE("Point token\n");
				GLdouble coords[3];
				for (j=0; j<3; j++)
				{
//					TRACE("%4.2f ",pBuf[rv-count]);
					coords[j]=pBuf[rv-count];
					count--;
				}
//				TRACE("\n");
//				gluUnProject(coords[0],coords[1],coords[2],
//					mvmatrix,projmatrix,viewport,
//					&wx,&wy,&wz);
//				TRACE("render coords = %lf, %lf, %lf; realworld coords = %lf, %lf, %lf\n",coords[0],coords[1],coords[2],wx,wy,wz);
				CPoint pt;
				pt.x=int(coords[0]);
				pt.y=int(viewport[3]-coords[1]-1);
				pInSel[id++]=rgn.PtInRegion(pt);
#ifdef _DEBUG
				TRACE("pt %d has hit=%d\n",id-1,pInSel[id-1]);
				if (rgn.PtInRegion(pt))
					hitCount++;
#endif
			}
		}

		m_bAutoScaleX=m_bAutoScaleY=m_bAutoScaleZ=FALSE;

// find max & min
		float xMax,yMax,zMax,xMin,yMin,zMin;
		xMax=yMax=zMax=-FLT_MAX;
		xMin=yMin=zMin=FLT_MAX;

// drawCount is index of points which are drawn within old axes, 
// and therefore appear in pSelList
		int drawCount=-1;	
		for (i=0; i<m_Count; i++)
		{
			if (!PtWithinAxes(m_pDat[i*3],m_pDat[i*3+1],m_pDat[i*3+2]))
				continue;
			drawCount++;
			if (pInSel[drawCount]==0)
				continue;
			xMax=max(m_pDat[i*3],xMax);
			xMin=min(m_pDat[i*3],xMin);
			yMax=max(m_pDat[i*3+1],yMax);
			yMin=min(m_pDat[i*3+1],yMin);
			zMax=max(m_pDat[i*3+2],zMax);
			zMin=min(m_pDat[i*3+2],zMin);
		}
		m_MaxX=NextAbove(xMax,5);
		m_MinX=NextBelow(xMin,5);
		m_MaxY=NextAbove(yMax,5);
		m_MinY=NextBelow(yMin,5);
		m_MaxZ=NextAbove(zMax,5);
		m_MinZ=NextBelow(zMin,5);
		Invalidate();

		delete [] pBuf;
		delete [] pInSel;
		rgn.DeleteObject();
	}
	m_bAllowMouseRotate=m_bOldAllowRotate;
	CancelSel();

#ifdef _DEBUG
TRACE("leaving ZoomSel, %d points on screen, %d in selection\n",id,hitCount);
#endif

	return TRUE;
}
