   M S B A M L   `   `   `  ����F  BDay08CarsDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Day08CarsDbDay08CarsDb  5   	       Day08CarsDb.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Day08CarsDb     5   	     � WindowStartupLocation$  CenterScreen=�5       � 
ResizeMode$ 	CanResize=�6?    � Title$ CarsDB��5   	   $	��350��6   $	��600��6%    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ ApplicationCommands.New��5
      6;     5   
   -   5        $  ApplicationCommands.SaveAs��5      6>     5   
   -   5        $ ApplicationCommands.Close��5      6=   .��5      ]� $��5q�6   .+�5   
   o� $	��Top=�6   @�5      m� 5      $
��File��6   .@�5      m� $��Export to CSV..��6    m� Command$  ApplicationCommands.SaveAs��68   m� 5      $
��Exit��6   $ ApplicationCommands.Close��6-   5      m� 5      $	��Add��6   $ ApplicationCommands.New��6(   5      �� 5   
   $��Bottom=�6   .@�5      �� .��5      �� -   5        $��	lblStatus��5      5      5      �� 5   
   -   5        $��
lstViewCar��5   (   $	��Top=�6    �� View 5      �� 5      .P�5       ��  �� Width$ 50��6%   $��Id��60    �� DisplayMemberBinding 6<   �� *Id+�� 5!      $	 190��6%   $��	MakeModel��61    6D   �� *	MakeModel+�� 5"      $	 180��6%   $��EngineSize(L)��61    6H   �� *
EngineSize+�� 5#      $	 180��6%   $��FuleType��61    6C   �� *
FuelType+5$      	 � ContextMenu	 5&      �� 5'      .@�5(      m� -   5        $��miUpdate��5(   G   $��Update��6   6G   �� 5)      m� 5*      -   5        $��miDelete��5*   G   $��Delete��6   6G   5+      5-      5.      5/      