   M S B A M L   `   `   `  ����N  JWallpaperDownloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:WallpaperDownloader.ViewModelWallpaperDownloader.ViewModel  5   	   (    "WallpaperDownloader.View.Wallpaper   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   A	viewModel+clr-namespace:WallpaperDownloader.ViewModel     5   	   -   5          � Title$  WallpaperDownloader��5   	   $	��660��6<   $	��530��6H    � 	Resources 5	      @  :WallpaperDownloader.ViewModel.WallpaperDownloaderViewModel   	ViewModel��%   &	          5
   
   61   5      .��5      � .+�5   
   �� -   5         � DataContext# [  5      ��6@   ��  �� Path$ Sprites���5      �� 5      .R�5      � 
���   �6    5      ��5      �� 5       �� 
TargetType" >�6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  �� Property �@K�ZIndex	 �� Value$	 99��6=   5      P� 5       �@J�IsMouseOver .  . �5      ��  �@K�ZIndex$	 0��6=   5      5      5      �� 5    
   -   5        
 ��Name$
 
prevButton��5       $��<��6%   $
��Left=�64   $��Center=�6O   $��17��6j   $��46��6u   #��� ��6�   $��0q�6�    J� Opacity$ 0Q�6�   6�   �� 5!   
   -   5        $
 
nextButton��5!      $��>��6%   $��Right=�64   $��Center=�6P   $��17��6k   $��46��6v   #��� ��6�   $��0q�6�   $ 0Q�6�   6�   5"      5#      