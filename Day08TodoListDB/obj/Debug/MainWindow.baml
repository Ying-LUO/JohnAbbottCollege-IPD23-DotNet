   M S B A M L   `   `   `  ����J  FDay08TodoListDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Day08TodoListDBDay08TodoListDB  5   	        Day08TodoListDB.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Day08TodoListDB     5   	     � WindowStartupLocation$  CenterScreen=�5       � 
ResizeMode$ 	CanResize=�6?    � Title$ TodoList��5   	   $	��450��6   $	��800��6'    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$  ApplicationCommands.SaveAs��5
      6>     5   
   -   5        $ ApplicationCommands.Close��5      6=   .��5      ]� $��5q�6   .+�5   
   o� $	��Top=�6   @�5      m� 5      $
��File��6   .@�5      m� $��Export to CSV..��6    m� Command$  ApplicationCommands.SaveAs��68   m� 5      $
��Exit��6   $ ApplicationCommands.Close��6-   5      5      �� 5   
   $��Bottom=�6   .@�5      �� .��5      �� -   5        $��	lblStatus��5      5      5      �� 5   
   -   5        $��lstView��5   )   $
��Left=�6    �� View 5      �� 5      .P�5      ��  �� Width$	 400��6%   $
��Task��61    �� DisplayMemberBinding 6?   �� *Task+�� 5      $	 140��6%   $��DueDate��61    6B   �� *	DueDate+	 �� StringFormat$	 {0:yyyy/MM/dd}���� 5       $	 100��6%   $��Status��61    6A   �� *Status+5!      
 � ContextMenu
 5#      �� 5$      .@�5%      m� -   5        $��miDelete��5%   G   $��Delete��6   6G   5&      5(      � 5)   
   -   5        $��
sortedList��5)   &   $��Right=�6    � RowDefinitions 5*      �� 5+      $
��auto �6    �� 5,      $
��auto �6    �� 5-      $
��auto �6    �� 5.      $
��auto �6    .+�50      �� -   5         ��Name$ btAdd��50   "   $��0��6   $	��Add��6E   $��80��6S   $
��0 10q�6^   �� 51      $��1��6   $��Sort By��6!   $	��0 5q�63   	� 52      -   5        $��2��52       	� 	GroupName$ SortBy��6'   $
��Task��6:   $
��30 5q�6I   6W   	� 53      -	   5        $��3��53      $ SortBy��6'   $��DueDate��6:   $
��30 5q�6L   6Z   54      55      56      