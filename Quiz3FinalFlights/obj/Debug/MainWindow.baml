   M S B A M L   `   `   `  ����L  HQuiz3FinalFlights, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Quiz3FinalFlightsQuiz3FinalFlights  5   	   "    Quiz3FinalFlights.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Quiz3FinalFlights     5   	     � WindowStartupLocation$  CenterScreen=�5       � 
ResizeMode$ 	CanResize=�6?    � Title$ Quiz3FinalFlights��5   	   $	��350��6#   $	��620��60    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ ApplicationCommands.New��5
      6;     5   
   -   5        $  ApplicationCommands.SaveAs��5      6>     5   
   -   5        $ ApplicationCommands.Close��5      6=   .��5      ]� $��5q�6   .+�5   
   o� $	��Top=�6   @�5      m� 5      $
��File��6   .@�5      m� $��Save Selected��6    m� Command$  ApplicationCommands.SaveAs��66   m� 5      $
��Exit��6   $ ApplicationCommands.Close��6-   5      m� 5      $	��Add��6   $ ApplicationCommands.New��6(   5      �� 5   
   $��Bottom=�6   .@�5      �� .��5      �� -   5        $��tbStatus��5      5      5      �� 5   
   -   5        $��lstView��5   )   $
��Left=�6    �� View 5      �� 5      .P�5       ��  �� Width$	 200��6%   $��OnDay��61    �� DisplayMemberBinding 6@   �� *OnDay+	 �� StringFormat$	 {0:yyyy/MM/dd}���� 5!      $	 100��6%   $��FromCode��61    6C   �� *
FromCode+�� 5"      $	 100��6%   $��ToCode��61    6A   �� *ToCode+�� 5#      $	 100��6%   $
��Type��61    6?   �� *Type+�� 5$      $	 100��6%   $��
Passengers��61    6E   �� *
Passengers+5%      
 � ContextMenu
 5'      �� 5(      .@�5)      m� -   5        $��miDelete��5)   G   $��Delete��6   6G   5*      5,      5-      5.      