   M S B A M L   `   `   `  ����  Cluster Analisys(    "Cluster_Analisys.ColorPickerDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$)  #Настройка кластера��5   	    � WindowStartupLocation$ CenterScreen=�6$    � 
ResizeMode$ NoResize=�5   	   $��500.667��6   $	��400��60   .��5      � $��0,0,-6,0q�6   $	��400��6   $	��473��6)   .+�5	   
   �� -   5        $��Window_Label��5	      $0��*Выберите цвет кластера��6$   $��113,-1,139,445q�6E   #  Xceed.Wpf.Toolkit.ColorCanvas  5   
   -   5          Name$ Color_Canvas��5      $��	0,52,10,0q�61   $	��390��6}   #  Xceed.Wpf.Toolkit.ColorPicker  5   
   -   5          Name$ Color_Picker��5      $��0,28,10,421q�61   6F   �� 5   
   -   5        $��Button_Accept��5   "    �� 	IsDefault . $��10,408,287,38q�6S   .��6j   Принять6s   �� 5   
    �� IsCancel . $��128,408,173,38q�6!    J� RenderTransformOrigin$ 0.5,0.5&�69   .��6Y   Отмена5      �� 5   
   -   5         ��Name$ Type_Selection��5      $
��Left=�6*   $��
10,333,0,0q�6E   $	��Top=�6Y   $	��217��6q   $��70��6�   5      �� 5   
   -   5        	 ��Name$	 
label_type��5      $E��?Выберите тип отображения кластера��6$   $
��Left=�6P   $��27��6k   $��
10,306,0,0q�6w   $	��Top=�6�   $	��217��6�   5      5      