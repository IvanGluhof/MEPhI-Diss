   M S B A M L   `   `   `  ����K  GCluster Analisys, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Cluster_AnalisysCluster_Analisys  5      !    Cluster_Analisys.MDS_Matrix   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Cluster_Analisys     5      $
��Left=�5      $	��Top=�6)   .��5	      (� .��5
   
   � $
��1856��6   $
��1020��6     � ColumnDefinitions  5      �� 5      $
��927* �6#   �� 5      .+�5      �� -   5         ��Name$ Matrix2D_Scroll��5      $��0,0,427,259q�64   $
��1427��6I   $	��761��6V   .��5      � -   5         �Name$ Matrix2D_MatrixPanel��5      $
��1427��6:    � Cursor$
 Hand��6G   $	��761��6U   5      � 5      -   5        $ Matrix_WorkPanel��5      $��1426,0,0,592q�62   $	��428��6H   $	��428��6T   
�������6a   &   System.Windows.Controls.DataGrid  5      -   5          Name$ DataGrid_Excel��5      $��-2,745,423,0q�6/      
IsReadOnly . 6E   �� 5      -   5         ��Name$ button_clear��5      $#��Снять выделение��6+   $
��Left=�6E   $��1432,433,0,0q�6`   $	��Top=�6v   $	��136��6�   $��33��6�   6�   5      5      5      