   M S B A M L   `   `   `  ����D  @SportShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SportShop.PagesSportShop.Pages  5      %    SportShop.Pages.ShopingCartPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:SportShop.Pages     5        N� Title$  ShopingCartPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   6*   �� 5      $
��auto �6   6,   �� 5      $��80 �6   6*   .+�5   
   �� $��10q�6   .+�5      �� $0��*Выберите пункт выдачи: ��6   �� 5      -   5         ��Name$ ComboPickup��5   �   $	��205��6   $
��Left=�6S   $��30��6n   $��14(�6z    �� DisplayMemberPath$ FullAddress��6�   5      �� 5   
   $��Корзина��6   $��Center=�6#   $��0��6>   $��Center=�6K   $
��24px(�6h   6z   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
DataGridSC��5      $��1��6'      AutoGenerateColumns .     	RowHeight$	 100Q�6P      
IsReadOnly .    SelectionMode$ Single=�6�   	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6)      Header$ Изображение��65      CellTemplate 5      �� 5      .R�5      �� $	��100��6E   ��6$   ��  �� Path$ GetImage�6E   5      5      0  *System.Windows.Controls.DataGridTextColumn  5      $* $Информация о товара��6I   $
 2* 6f   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *ProductFullInfo+6f     5       $  Производитель��6K   $
 1* 6b    6%   �� *Manufacturer.Name+6b     5!      $

 auto 6)   $ Колл-во��66    5"      �� 5#      .R�5$      ��  ��Name$ 	countText��6F   -   5        ��5$   Z   �� *ProductQuantityInStock+6�   5%      5'        5(      $ Скидка��6Q   $

 auto 6a    6%   �� *ProductDiscountAmount+6a     5)      $" Стоимость за шт��6}   $

 auto 6�    6%   �� *ProductCostWithDiscount+ �� StringFormat$ c�� �� ConverterCulture$ ru-RU��6�     5*      $

 auto 6)   $  ��66    5+      �� 5,      .R�5-      ��  ��Name$ 	BtnDelete��6%   -   5        $��Удалить��5-   8   $��0q�6b   5.      50      52      �� 53   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0 0 30 0q�6W   .+�54      �� $ 
Horizontal=�6   $��10q�62   .+�55      �� $��Center=�6   $��20(�67   $��
Итог: ��6E   6U   �� 56      -   5         ��Name$ TotalSum��56   E   $��Center=�6   $��20(�67   $
��0 р��6W   6d   57      �� 58      -   5        $��Оплатить��58      $	��100��6<   $��40��6H   $��20 10 10 10q�6T   6k   59      5:      5;      