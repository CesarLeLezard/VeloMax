   M S B A M L   `   `   `  ����B  >Velomax, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:VelomaxVelomax  5   	       Velomax.Commandes   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Velomax     5   	     � Title$  	Commandes��5   	   $	��800��6   $
��1200��6(   ���	#FFD9D9D9 � WindowStartupLocation$ CenterScreen=�6L    � 
ResizeMode$ CanMinimize=�6q   .��5	      �� .+�5
   
   �� -   5         ��Name$ lTitre��5
      $��	Commandes��6    $
��Left=�64   $��90��6O   $	��Top=�6[   $
��1174��6s   $��Courier New)�6�   $��72(�6�   ���	#FFC33F23���	#FF299ACF J� RenderTransformOrigin$ 0.5,0.5&�6�    �� Left$ 10��6  
 �� Top$ 30��6  $��Center=�6-  ��6�   Z�  J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ 	#FF198397��6#   z�5      V� 5      .~�5      �� 	 �� ScaleY$	 1Q�6%   
 �� ScaleX$
 1Q�60   �� 5       �� AngleY$ 0Q�6$    �� AngleX$ 0Q�6/   �� 5       �� Angle$ 0Q�6&   T� 5      5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgCommandes��5      $	��406��6(   $ 51��65   $	 201��6F   $
��1085��6W      AutoGenerateColumns .  $��16(�6�      HorizontalGridLinesBrush �	#FFB6B6B6   VerticalGridLinesBrush �	#FFB6B6B6   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID commande��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 120 6Z      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *id_commande+   HeaderStyle 5      �� 5       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5      ��  �� Property � �� �� Value$ Center=�6K   5      5        5       $
 Date��6%   $	 150 6t    .  63   �� *date_commande+ �� StringFormat$ dd/MM/yyyy HH:mm�� 5!      �� 5"      "  6    .�5#      ��  � ��$ Center=�6K   5$         ElementStyle 5&      �� 5'      " ��6    .�5(      ��  � ��$ Center=�6D   5)      5+        5,      $ Adresse livraison��6%   $	 250 6n    .  6@   �� *adresseLivraison_commande+ 5-      �� 5.      "  6    .�5/      ��  � ��$ Center=�6K   50       52      �� 53      " ��6    .�54      ��  � ��$
 Left=�6D   �� 55       � ��$ 5,0,0,0q�67   56      58        59      $ Code postal ��6%   $	 110 6g    .  6;   �� *codePLivraison_commande+ 5:      �� 5;      "  6    .�5<      ��  � ��$ Center=�6K   5=       5?      �� 5@      " ��6    .�5A      ��  � ��$
 Left=�6D   �� 5B       � ��$ 5,0,0,0q�67   5C      5E        5F      $ Ville livraison��6%   $	 150 6j    .  6>   �� *villeLivraison_commande+ 5G      �� 5H      "  6    .�5I      ��  � ��$ Center=�6K   5J       5L      �� 5M      " ��6    .�5N      ��  � ��$
 Left=�6D   �� 5O       � ��$ 5,0,0,0q�67   5P      5R        5S      $ Date livraison��6%   $	 150 6�    .  6=   �� *dateLivraison_commande+$ dd/MM/yyyy HH:mm�� 5T      �� 5U      "  6    .�5V      ��  � ��$ Center=�6K   5W       5Y      �� 5Z      " ��6    .�5[      ��  � ��$ Center=�6D   5\      5^        5_      $ Montant��6%   $	 120 6x    .  66   �� *total+$ C�� �� ConverterCulture$ fr-FR�� 5`      �� 5a      "  6    .�5b      ��  � ��$ Center=�6K   5c       5e      �� 5f      " ��6    .�5g      ��  � ��$ Right=�6D   �� 5h       � ��$ 0,0,5,0q�67   �� 5i       � ��$
 Bold"�6;   5j      5l      5n      �� 5o   
   -   5         ��Name$ bNvCommande��5o      $��Nouvelle commande��6&   $
��Left=�6B   $��60��6]   $	��Top=�6i   $	��325��6�   $��36(�6�   $��Corbel Light)�6�   ���	#FF299ACF���	#FFC33F23 � Cursor$
 Hand��6�   $ 51��6�   $	 659��6   5p        5q      $ 	#FFC33F23��6#   5s      �� 5t   
   -   5        $ bDetailsCommande��5t      $��Détails��6+   $
��Left=�6=   $��60��6X   $	��Top=�6d   $	��240��6|   $��36(�6�   $��Corbel Light)�6�   ���	#FF299ACF���	#FFC33F23$
 Hand��6�   $	 513��6�   $	 659��6�    5u        5v      $ 	#FFC33F23��6#   5x      �� 5y   
   -   5        $ 
bSupprimer��5y      $��	Supprimer��6%   $
��Left=�69   $��60��6T   $	��Top=�6`   $	��240��6x   $��36(�6�   $��Corbel Light)�6�   ���	#FF299ACF���	#FFC33F23$
 Hand��6�   $	 896��6�   $	 659��6�    5z        5{      $ 	#FFC33F23��6#   5}      5      5�      