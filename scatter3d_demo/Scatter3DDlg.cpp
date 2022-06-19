// Scatter3DDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Scatter3D.h"
#include "Scatter3DDlg.h"

#include "GLSelectableScatterGraph.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CScatter3DDlg dialog

CScatter3DDlg::CScatter3DDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CScatter3DDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CScatter3DDlg)
	m_ProjType = 0;
	m_SymbolSize = 5;
	m_bMouseRotate = TRUE;
	m_MaxX = 1.0f;
	m_MaxY = 1.0f;
	m_MaxZ = 1.0f;
	m_MinX = 0.0f;
	m_MinY = 0.0f;
	m_MinZ = 0.0f;
	m_bAutoX = TRUE;
	m_bAutoY = TRUE;
	m_bAutoZ = TRUE;
	m_DatCount = 4;
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pDisplay = new CGLSelectableScatterGraph;
	m_ClearCol=RGB(0,0,0);
	m_PtCol=RGB(255,0,0);

	m_pData=NULL;
	m_pColList=NULL;
}

void CScatter3DDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CScatter3DDlg)
	DDX_Radio(pDX, IDC_PROJ_TYPE, m_ProjType);
	DDX_Text(pDX, IDC_SYMBOL_SIZE, m_SymbolSize);
	DDV_MinMaxInt(pDX, m_SymbolSize, 1, 20);
	DDX_Check(pDX, IDC_MOUSE_ROTATE, m_bMouseRotate);
	DDX_Text(pDX, IDC_MAX_X, m_MaxX);
	DDX_Text(pDX, IDC_MAX_Y, m_MaxY);
	DDX_Text(pDX, IDC_MAX_Z, m_MaxZ);
	DDX_Text(pDX, IDC_MIN_X, m_MinX);
	DDX_Text(pDX, IDC_MIN_Y, m_MinY);
	DDX_Text(pDX, IDC_MIN_Z, m_MinZ);
	DDX_Check(pDX, IDC_AUTO_X, m_bAutoX);
	DDX_Check(pDX, IDC_AUTO_Y, m_bAutoY);
	DDX_Check(pDX, IDC_AUTO_Z, m_bAutoZ);
	DDX_Text(pDX, IDC_DAT_COUNT, m_DatCount);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CScatter3DDlg, CDialog)
	//{{AFX_MSG_MAP(CScatter3DDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_SIZE()
	ON_BN_CLICKED(IDC_PROJ_TYPE, OnProjType)
	ON_BN_CLICKED(IDC_PROJ_TYPE2, OnProjType2)
	ON_BN_CLICKED(IDC_BACK_COLOUR, OnBackColour)
	ON_WM_DRAWITEM()
	ON_EN_KILLFOCUS(IDC_SYMBOL_SIZE, OnKillfocusSymbolSize)
	ON_BN_CLICKED(IDC_MOUSE_ROTATE, OnMouseRotate)
	ON_BN_CLICKED(IDC_AUTO_X, OnAutoX)
	ON_BN_CLICKED(IDC_AUTO_Y, OnAutoY)
	ON_BN_CLICKED(IDC_AUTO_Z, OnAutoZ)
	ON_EN_KILLFOCUS(IDC_MAX_X, OnKillfocusMaxX)
	ON_EN_KILLFOCUS(IDC_MAX_Y, OnKillfocusMaxY)
	ON_EN_KILLFOCUS(IDC_MAX_Z, OnKillfocusMaxZ)
	ON_EN_KILLFOCUS(IDC_MIN_X, OnKillfocusMinX)
	ON_EN_KILLFOCUS(IDC_MIN_Y, OnKillfocusMinY)
	ON_EN_KILLFOCUS(IDC_MIN_Z, OnKillfocusMinZ)
	ON_BN_CLICKED(IDC_MAKE_SEL, OnMakeSel)
	ON_BN_CLICKED(IDC_CANCEL_SEL, OnCancelSel)
	ON_BN_CLICKED(IDC_ZOOM_SEL, OnZoomSel)
	ON_BN_CLICKED(IDC_COPY, OnCopy)
	ON_BN_CLICKED(IDC_LOAD, OnLoad)
	ON_WM_DROPFILES()
	ON_BN_CLICKED(IDC_PT_COLOUR, OnPtColour)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CScatter3DDlg message handlers

BOOL CScatter3DDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// Enable drag/drop open
	DragAcceptFiles();

	
	CRect rect(10,10,10,10);

	// TODO: Add extra initialization here
	m_pDisplay->Create( NULL,   //CWnd default
						NULL,   //has no name
						WS_CHILD|WS_CLIPSIBLINGS|WS_CLIPCHILDREN|WS_VISIBLE,
						rect,
						this,   //this is the parent
						0);     //this should really be a different number... check resource.h

// generate OnSize to get Graph window positioned right
	CRect r;
	GetWindowRect(&r);
	r.InflateRect(1,1);
    MoveWindow(r);

	m_pDisplay->SetProjection(m_ProjType);
	m_pDisplay->SetSymbolSize(m_SymbolSize);
	m_pDisplay->SetClearCol(m_ClearCol);
	m_pDisplay->AllowMouseRotate(m_bMouseRotate);	

	static float fake_data[]= {
       0.,			0.,	      0.,	
       1.,	      1.,	       1.,	
       0.5,	      1,	      0.,
	   0.5,			0.5,		0.5
	   };
	static COLORREF fake_colList[] = {
		RGB(255,0,0),	// red
		RGB(255,255,0),	// yellow
		RGB(0,255,255),	// cyan
		RGB(255,0,255)
	};

	m_pDisplay->SetData(4,RGB(255,0,0),fake_data,fake_colList);
	OnAutoX();	// scale data and fill edit boxes
	OnAutoY();
	OnAutoZ();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CScatter3DDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CScatter3DDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

CScatter3DDlg::~CScatter3DDlg()
{
	if(m_pDisplay)
	{
		delete m_pDisplay;
	}
	if (m_pData!=NULL)
		delete [] m_pData;
	if (m_pColList!=NULL)
		delete [] m_pColList;
}

void CScatter3DDlg::OnSize(UINT nType, int cx, int cy) 
{
	CDialog::OnSize(nType, cx, cy);
	
	if (m_pDisplay->GetSafeHwnd()==NULL)		// OnSize is first called before Buttons created??
		return;

	CRect R;
    GetDlgItem(IDC_GRAPH_TOPLEFT)->GetWindowRect(R);		// get dimensions of Button
    ScreenToClient(R);
//    R.left=R.right+10;
//    R.top=10;
    R.right=cx-10;
    R.bottom=cy-10;
    m_pDisplay->MoveWindow(R);
}

void CScatter3DDlg::OnProjType() 
{
	UpdateData();
	m_pDisplay->SetProjection(m_ProjType);
	m_pDisplay->Invalidate(FALSE);
}

void CScatter3DDlg::OnProjType2() 
{
	UpdateData();
	m_pDisplay->SetProjection(m_ProjType);
	m_pDisplay->Invalidate(FALSE);
}

void CScatter3DDlg::OnBackColour() 
{
	CColorDialog dlg;
	if (dlg.DoModal()!=IDOK)
		return;

	m_ClearCol=dlg.GetColor();	// for drawing button
	GetDlgItem(IDC_BACK_COLOUR)->Invalidate();
	m_pDisplay->SetClearCol(m_ClearCol);
}

void CScatter3DDlg::OnDrawItem(int nIDCtl, LPDRAWITEMSTRUCT lpDrawItemStruct) 
{
	if (nIDCtl ==IDC_BACK_COLOUR || nIDCtl ==IDC_PT_COLOUR)
	{
		if (lpDrawItemStruct->itemAction & ODA_DRAWENTIRE)
		{
			HBRUSH hBrush;
			if (nIDCtl ==IDC_BACK_COLOUR)
				hBrush=::CreateSolidBrush(m_ClearCol);
			else
				hBrush=::CreateSolidBrush(m_PtCol);
	
			::FillRect(lpDrawItemStruct->hDC,&(lpDrawItemStruct->rcItem),
				hBrush);
			::DeleteObject(hBrush);
		}
		else if (lpDrawItemStruct->itemAction & ODA_FOCUS)
		{
			RECT focusR=lpDrawItemStruct->rcItem;
			focusR.top+=2;
			focusR.bottom-=2;
			focusR.left+=2;
			focusR.right-=2;
			::DrawFocusRect(lpDrawItemStruct->hDC,&focusR);
		}
	return;		// eat it		
	}		
	
	CDialog::OnDrawItem(nIDCtl, lpDrawItemStruct);
}

void CScatter3DDlg::OnKillfocusSymbolSize() 
{
	if (!UpdateData())
		return;
	m_pDisplay->SetSymbolSize(m_SymbolSize);
}

void CScatter3DDlg::OnMouseRotate() 
{
	UpdateData();
	m_pDisplay->AllowMouseRotate(m_bMouseRotate);	
}

void CScatter3DDlg::OnAutoX() 
{
	UpdateData();
	m_pDisplay->SetAutoScaleX(m_bAutoX);
	m_MaxX=m_pDisplay->GetMaxX();
	m_MinX=m_pDisplay->GetMinX();
	UpdateData(FALSE);
	((CEdit *)GetDlgItem(IDC_MIN_X))->SetReadOnly(m_bAutoX);
	((CEdit *)GetDlgItem(IDC_MAX_X))->SetReadOnly(m_bAutoX);
}

void CScatter3DDlg::OnAutoY() 
{
	UpdateData();
	m_pDisplay->SetAutoScaleY(m_bAutoY);
	m_MaxY=m_pDisplay->GetMaxY();
	m_MinY=m_pDisplay->GetMinY();
	UpdateData(FALSE);
	((CEdit *)GetDlgItem(IDC_MIN_Y))->SetReadOnly(m_bAutoY);
	((CEdit *)GetDlgItem(IDC_MAX_Y))->SetReadOnly(m_bAutoY);
}

void CScatter3DDlg::OnAutoZ() 
{
	UpdateData();
	m_pDisplay->SetAutoScaleZ(m_bAutoZ);
	m_MaxZ=m_pDisplay->GetMaxZ();
	m_MinZ=m_pDisplay->GetMinZ();
	UpdateData(FALSE);
	((CEdit *)GetDlgItem(IDC_MIN_Z))->SetReadOnly(m_bAutoZ);
	((CEdit *)GetDlgItem(IDC_MAX_Z))->SetReadOnly(m_bAutoZ);
}

void CScatter3DDlg::OnKillfocusMaxX() 
{
	UpdateData();
	m_pDisplay->SetMaxX(m_MaxX);
}

void CScatter3DDlg::OnKillfocusMaxY() 
{
	UpdateData();
	m_pDisplay->SetMaxY(m_MaxY);
}

void CScatter3DDlg::OnKillfocusMaxZ() 
{
	UpdateData();
	m_pDisplay->SetMaxZ(m_MaxZ);
}

void CScatter3DDlg::OnKillfocusMinX() 
{
	UpdateData();
	m_pDisplay->SetMinX(m_MinX);
}

void CScatter3DDlg::OnKillfocusMinY() 
{
	UpdateData();
	m_pDisplay->SetMinY(m_MinY);
}

void CScatter3DDlg::OnKillfocusMinZ() 
{
	UpdateData();
	m_pDisplay->SetMinZ(m_MinZ);
}

void CScatter3DDlg::OnMakeSel() 
{
	m_bOldAllowRotate=m_bMouseRotate;
	m_bMouseRotate=FALSE;
	UpdateData(FALSE);
	GetDlgItem(IDC_MOUSE_ROTATE)->EnableWindow(FALSE);
	m_pDisplay->StartMakeSel();
}

void CScatter3DDlg::OnCancelSel() 
{
	m_pDisplay->CancelSel();
	m_bMouseRotate=m_bOldAllowRotate;
	GetDlgItem(IDC_MOUSE_ROTATE)->EnableWindow();
	UpdateData(FALSE);
}

void CScatter3DDlg::OnZoomSel() 
{
	m_pDisplay->ZoomSel();
	m_bAutoX=m_bAutoY=m_bAutoZ=FALSE;
	m_bMouseRotate=m_bOldAllowRotate;
	GetDlgItem(IDC_MOUSE_ROTATE)->EnableWindow();
	m_MaxX=m_pDisplay->GetMaxX();
	m_MinX=m_pDisplay->GetMinX();
	m_MaxY=m_pDisplay->GetMaxY();
	m_MinY=m_pDisplay->GetMinY();
	m_MaxZ=m_pDisplay->GetMaxZ();
	m_MinZ=m_pDisplay->GetMinZ();
	UpdateData(FALSE);
	((CEdit *)GetDlgItem(IDC_MIN_X))->SetReadOnly(m_bAutoX);
	((CEdit *)GetDlgItem(IDC_MAX_X))->SetReadOnly(m_bAutoX);
	((CEdit *)GetDlgItem(IDC_MIN_Y))->SetReadOnly(m_bAutoY);
	((CEdit *)GetDlgItem(IDC_MAX_Y))->SetReadOnly(m_bAutoY);
	((CEdit *)GetDlgItem(IDC_MIN_Z))->SetReadOnly(m_bAutoZ);
	((CEdit *)GetDlgItem(IDC_MAX_Z))->SetReadOnly(m_bAutoZ);

}


void CScatter3DDlg::OnCopy() 
{
	m_pDisplay->CopyToClipboard();
}

void CScatter3DDlg::OnLoad() 
{
	CFileDialog dlg(TRUE,NULL,NULL);
	if (dlg.DoModal()!=IDOK)
		return;

	DoOpen(dlg.GetPathName());
}


void CScatter3DDlg::OnDropFiles(HDROP hDropInfo) 
{
	char fName[1000];
	::DragQueryFile(hDropInfo,0,fName,1000);
TRACE("File %s got dropped here\n",fName);

	CDialog::OnDropFiles(hDropInfo);

	DoOpen(fName);
}

#define STRLEN 1000
BOOL CScatter3DDlg::DoOpen(CString fName)
{
	CString errStr;
	int i,j,rv;
	char line[STRLEN];
	FILE *fp;
	fp = fopen(fName, "r");
	if (!fp) 
	{
		AfxMessageBox("Couldn't open input file",MB_ICONWARNING);
		return FALSE;
	}
	fgets(line,STRLEN,fp);
	m_DatCount=1;
// count dimensions (number of floats in line)
	int dimCount=0;
	BOOL bInFlt=FALSE;
	for (i=0; i<int(strlen(line)); i++)
	{
		if (isspace(line[i]))
		{
			if (bInFlt)
			{
				bInFlt=FALSE;
				dimCount++;
			}
		}
		else
			bInFlt=TRUE;
	}

	if (dimCount!=3 && dimCount!=4 && dimCount!=6)
	{
		errStr.Format("file has %d columns, but should have 3, 4 or 6",dimCount);
		AfxMessageBox(errStr,MB_ICONWARNING);
		fclose(fp);
		return FALSE;
	}

// find number of lines
	while (fgets(line, STRLEN, fp))
		m_DatCount++;

	if (m_pData!=NULL)
		delete [] m_pData;
	if (m_pColList!=NULL)
	{
		delete [] m_pColList;
		m_pColList=NULL;
	}

	m_pData=new float[m_DatCount*3];
	if (dimCount!=3)
		m_pColList=new COLORREF[m_DatCount];

// rewind file
	fseek(fp, 0, SEEK_SET);

	// load data
	int readCount=0;
	int col[3];
	for (i=0; i<m_DatCount; i++) 
	{
		for (j=0; j<3; j++)
		{
			if ((rv=fscanf(fp, "%f", &(m_pData[i*3+j])))!=1)
				goto error;
		}
		if (dimCount==4)	// COLORREF as last item
		{
			if ((rv=fscanf(fp, "%u", &(m_pColList[i])))!=1)
				goto error;
		}
		else if (dimCount==6)	// r,g,b given separately
		{
			for (j=0; j<3; j++)
			{
				if ((rv=fscanf(fp, "%u", &(col[j])))!=1)
					goto error;
			}
			m_pColList[i]=RGB(col[0],col[1],col[2]);
		}
		else
		{
			ASSERT(dimCount==3);
			;
		}
	}
	fclose(fp);
	m_pDisplay->SetData(m_DatCount,RGB(255,0,0),m_pData,m_pColList);	
	UpdateData(FALSE);	// update count

	if (dimCount==3)
	{
		GetDlgItem(IDC_PT_COL_TXT)->EnableWindow();
		GetDlgItem(IDC_PT_COLOUR)->ShowWindow(SW_SHOWNA);
	}
	else
	{
		GetDlgItem(IDC_PT_COL_TXT)->EnableWindow(FALSE);
		GetDlgItem(IDC_PT_COLOUR)->ShowWindow(SW_HIDE);
	}

	return TRUE;

error:
	AfxMessageBox("Error reading data file");
	fclose(fp);
	return FALSE;
}

void CScatter3DDlg::OnPtColour() 
{
	CColorDialog dlg;
	if (dlg.DoModal()!=IDOK)
		return;

	m_PtCol=dlg.GetColor();	// for drawing button
	GetDlgItem(IDC_PT_COLOUR)->Invalidate();
	m_pDisplay->SetPtCol(m_PtCol);
}
