// Scatter3DDlg.h : header file
//

#if !defined(AFX_SCATTER3DDLG_H__82DC1E4C_E277_4BBF_820E_19980F76C2B6__INCLUDED_)
#define AFX_SCATTER3DDLG_H__82DC1E4C_E277_4BBF_820E_19980F76C2B6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CScatter3DDlg dialog



/////////////////////////////////////////////////////////////////////////////
// CScatter3DDlg dialog
class CGLSelectableScatterGraph;
class CScatter3DDlg : public CDialog
{
// Construction
public:
	COLORREF m_ClearCol;
	~CScatter3DDlg(void);
	CScatter3DDlg(CWnd* pParent = NULL);	// standard constructor
	CGLSelectableScatterGraph*m_pDisplay;

// Dialog Data
	//{{AFX_DATA(CScatter3DDlg)
	enum { IDD = IDD_SCATTER3D_DIALOG  };
	int		m_ProjType;
	int		m_SymbolSize;
	BOOL	m_bMouseRotate;
	float	m_MaxX;
	float	m_MaxY;
	float	m_MaxZ;
	float	m_MinX;
	float	m_MinY;
	float	m_MinZ;
	BOOL	m_bAutoX;
	BOOL	m_bAutoY;
	BOOL	m_bAutoZ;
	int		m_DatCount;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CScatter3DDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	COLORREF m_PtCol;
	BOOL DoOpen(CString fName);
	float *m_pData;
	COLORREF *m_pColList;

	BOOL m_bOldAllowRotate;
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CScatter3DDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnProjType();
	afx_msg void OnProjType2();
	afx_msg void OnBackColour();
	afx_msg void OnDrawItem(int nIDCtl, LPDRAWITEMSTRUCT lpDrawItemStruct);
	afx_msg void OnKillfocusSymbolSize();
	afx_msg void OnMouseRotate();
	afx_msg void OnAutoX();
	afx_msg void OnAutoY();
	afx_msg void OnAutoZ();
	afx_msg void OnKillfocusMaxX();
	afx_msg void OnKillfocusMaxY();
	afx_msg void OnKillfocusMaxZ();
	afx_msg void OnKillfocusMinX();
	afx_msg void OnKillfocusMinY();
	afx_msg void OnKillfocusMinZ();
	afx_msg void OnMakeSel();
	afx_msg void OnCancelSel();
	afx_msg void OnZoomSel();
	afx_msg void OnCopy();
	afx_msg void OnLoad();
	afx_msg void OnDropFiles(HDROP hDropInfo);
	afx_msg void OnPtColour();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SCATTER3DDLG_H__82DC1E4C_E277_4BBF_820E_19980F76C2B6__INCLUDED_)
