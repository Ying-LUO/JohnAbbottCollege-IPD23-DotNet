   M S B A M L   `   `   `  ����J  FDay08TodoListDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Day08TodoListDBDay08TodoListDB  5   	   '    !Day08TodoListDB.AddEditTodoDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Day08TodoListDB     5   	     � WindowStartupLocation$  CenterOwner=�5       � Title$ AddEditTodoDialog��5   	   $	��240��6#   $	��500��60   .��5	      �  � 	Resources 5
   
   (  "System.Windows.Controls.DatePicker �� 
TargetType �� Property �� Value��%�   '        '�� O     �� 5      "  6   .�5      ��  � ��$ 	0 10 20 0q�6+   5      �� 5      " ��6   .�5      ��  � ��$	 100��6*   �� 5       � ��$
 0 30q�6+   5      5       � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $	��150 �6   �� 5      $	��300 �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��20 �6   .+�5!   
   �� $��0��6   $��1��6   $��Id��6-   �� 5"   
   -   5        $
��tbId��5"   1   $��0��6   $��2��6!   ��6=   �� *Id+�� 5#   
   $��1��6   $��1��6   $
��Task��6-   �� 5$   
   -   5         ��Name$ tbTask��5$   /   $��1��6   $��2��6   	 �� 	MaxLength$	 50��6?   �� 5%   
   $��2��6   $��1��6   $��Due Date��6-     5&   
   -   5        $��	dpDueDate��5&   F   $��2��6   $��2��6"   $��2��62   
   DisplayDateStart$
 
1900/01/01X�5'         DisplayDateEnd$ 
2100/01/01X�67   �� 5(   
   $��3��6   $��1��6   $��Is Done��6-   �� 5)   
   -   5        $��ckbDone��5)   K   $��3��6   $��2��6    $��Center=�60   6K   �� 5*   
   -   5         ��Name$ btCancel��5*   .   $��4��6   $��1��6   $��Cancel��6@    �� IsCancel . 6Q   �� 5+   
   -   5        $ btSave��5+   .   $��4��6   $��2��6   $
��Save��6>    �� 	IsDefault . 6^   5,      5-      