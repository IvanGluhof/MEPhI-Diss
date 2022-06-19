// Scatter3D.h : main header file for the SCATTER3D application
//

#if !defined(AFX_SCATTER3D_H__E7FA480E_F40B_46C6_86BC_9245DD068D44__INCLUDED_)
#define AFX_SCATTER3D_H__E7FA480E_F40B_46C6_86BC_9245DD068D44__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CScatter3DApp:
// See Scatter3D.cpp for the implementation of this class
//

class CScatter3DApp : public CWinApp
{
public:
	CScatter3DApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CScatter3DApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CScatter3DApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SCATTER3D_H__E7FA480E_F40B_46C6_86BC_9245DD068D44__INCLUDED_)
