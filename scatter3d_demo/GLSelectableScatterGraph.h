#if !defined(AFX_GLSELECTABLESCATTERGRAPH_H__40C340B8_32D2_4047_92CF_10C2074C9B5E__INCLUDED_)
#define AFX_GLSELECTABLESCATTERGRAPH_H__40C340B8_32D2_4047_92CF_10C2074C9B5E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// GLSelectableScatterGraph.h : header file
//
#include "GLScatterGraph.h"
#include <afxtempl.h>
/////////////////////////////////////////////////////////////////////////////
// CGLSelectableScatterGraph window

class CGLSelectableScatterGraph : public CGLScatterGraph
{
// Construction
public:
	CGLSelectableScatterGraph();

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CGLSelectableScatterGraph)
	//}}AFX_VIRTUAL

// Implementation
public:
	BOOL ZoomSel();
	void CancelSel();
	void StartMakeSel();
	virtual ~CGLSelectableScatterGraph();

protected:
	BOOL m_bOldAllowRotate;
	BOOL m_bMakeSel;
	CPoint m_PrevPt;	
	CArray<CPoint, CPoint> m_SelPts;

	virtual void OnDrawGDI(CPaintDC *pDC); // override to issue GDI drawing functions
	// Generated message map functions
protected:
	//{{AFX_MSG(CGLSelectableScatterGraph)
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_GLSELECTABLESCATTERGRAPH_H__40C340B8_32D2_4047_92CF_10C2074C9B5E__INCLUDED_)
