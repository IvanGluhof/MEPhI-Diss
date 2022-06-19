; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CScatter3DDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "scatter3d.h"
LastPage=0

ClassCount=6
Class1=CGLMouseRotate
Class2=CGLScatterGraph
Class3=CGLSelectableScatterGraph
Class4=COpenGLWnd
Class5=CScatter3DApp
Class6=CScatter3DDlg

ResourceCount=1
Resource1=IDD_SCATTER3D_DIALOG

[CLS:CGLMouseRotate]
Type=0
BaseClass=COpenGLWnd
HeaderFile=GLMouseRotate.h
ImplementationFile=GLMouseRotate.cpp

[CLS:CGLScatterGraph]
Type=0
BaseClass=CGLMouseRotate
HeaderFile=GLScatterGraph.h
ImplementationFile=GLScatterGraph.cpp

[CLS:CGLSelectableScatterGraph]
Type=0
BaseClass=CGLScatterGraph
HeaderFile=GLSelectableScatterGraph.h
ImplementationFile=GLSelectableScatterGraph.cpp

[CLS:COpenGLWnd]
Type=0
BaseClass=CWnd
HeaderFile=OpenGLWnd.h
ImplementationFile=OpenGLWnd.cpp

[CLS:CScatter3DApp]
Type=0
BaseClass=CWinApp
HeaderFile=Scatter3D.h
ImplementationFile=Scatter3D.cpp

[CLS:CScatter3DDlg]
Type=0
BaseClass=CDialog
HeaderFile=Scatter3DDlg.h
ImplementationFile=Scatter3DDlg.cpp
LastObject=IDC_PT_COLOUR
Filter=D
VirtualFilter=dWC

[DLG:IDD_SCATTER3D_DIALOG]
Type=1
Class=CScatter3DDlg
ControlCount=37
Control1=IDOK,button,1342242817
Control2=IDC_GRAPH_TOPLEFT,static,1073741831
Control3=IDC_PROJ_TYPE,button,1342308361
Control4=IDC_PROJ_TYPE2,button,1342177289
Control5=IDC_STATIC,button,1342308359
Control6=IDC_BACK_COLOUR,button,1342242827
Control7=IDC_STATIC,static,1342308352
Control8=IDC_SYMBOL_SIZE,edit,1350631552
Control9=IDC_STATIC,static,1342308352
Control10=IDC_MOUSE_ROTATE,button,1342242819
Control11=IDC_STATIC,button,1342177287
Control12=IDC_AUTO_X,button,1342242819
Control13=IDC_AUTO_Y,button,1342242819
Control14=IDC_AUTO_Z,button,1342242819
Control15=IDC_MIN_X,edit,1350631552
Control16=IDC_STATIC,static,1342308352
Control17=IDC_MAX_X,edit,1350631552
Control18=IDC_MIN_Y,edit,1350631552
Control19=IDC_MIN_Z,edit,1350631552
Control20=IDC_MAX_Z,edit,1350631552
Control21=IDC_MAX_Y,edit,1350631552
Control22=IDC_STATIC,static,1342308352
Control23=IDC_STATIC,static,1342308352
Control24=IDC_STATIC,static,1342308352
Control25=IDC_STATIC,static,1342308352
Control26=IDC_STATIC,static,1342308352
Control27=IDC_MAKE_SEL,button,1342242817
Control28=IDC_CANCEL_SEL,button,1342242817
Control29=IDC_SEL_FRAME,button,1342177287
Control30=IDC_ZOOM_SEL,button,1342242817
Control31=IDC_LOAD,button,1342242816
Control32=IDC_STATIC,static,1342308352
Control33=IDC_DAT_COUNT,edit,1350633600
Control34=IDC_COPY,button,1342242816
Control35=IDC_STATIC,static,1342308352
Control36=IDC_PT_COL_TXT,static,1342308352
Control37=IDC_PT_COLOUR,button,1342242827

