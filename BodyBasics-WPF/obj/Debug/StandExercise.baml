   M S B A M L   `   `   `  ����  BodyBasics-WPF7    1Microsoft.Samples.Kinect.BodyBasics.StandExercise   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Body Basics��5   	   $	��600��5   	   $	��700��6    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush��%�   &	        &	 #     &	 F     �� 5	   
   $ 	#ff6e6e6e��62   �� 5
   
   $ 	#ff52318f��64   �� 5   
   $ 	#ff00BCF2��62   5      .��5      � $��	10 0 10 0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Images\Logo.png��6   $
��Left=�66   $
��Fill=�6Q   $��32��6`   $��81��6l   $��0 10 0 5q�6w   �� 5   
   $��0��6   $��0 0 -1 0q�6!   $��Right=�63   $��Bottom=�6O   #��[  6j   $��Segoe UI)�6�   $��18(�6�   $��92��6�   	Free Test6�   �� 5   
   $��0��6   $��Images\Status.png��6   $
��None=�68   $��Center=�6G   $��0 0 0 5q�6d   (� 5   
   $��1��6   $��Center=�6   .��5      �� $��UniformToFill=�63   ��6   �� *ImageSource+63   5      �� 5   
   -   5        $��	statusBar��5   ?   $��2��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   .@�5      �� ��6   �� *
StatusText+5      �� 5   
   -   5         ��Name$ 
Right_Hand��5      $
��Left=�6I   $��0,20,0,0q�6d   $	��Top=�6v    J� RenderTransformOrigin$ -0.441,3.115&�6�   $��1��6�   ���	#FFCF55D4��6%   �� *RightHandValues+��6�   Z� 6�   �� 5    
   -   5        $ Right_Elbow��5       $��Right=�6J   $��
510,20,0,0q�6f   $ -0.441,3.115&�6z   $��1��6�   ���	#FFCF55D4$	��Top=�6�   ��6%   �� *RightElbowValues+��6�   Z� 6�   �� 5!   
   -   5        $ Right_Elbow_Angles��5!      $
��Left=�6Q   $��
326,20,0,0q�6l   $ -0.441,3.115&�6�   $��1��6�   ���	#FFCF55D4$	��Top=�6�   ��6,   �� *RightElbowAngles+��6�   Z� 6�   5"      5#      