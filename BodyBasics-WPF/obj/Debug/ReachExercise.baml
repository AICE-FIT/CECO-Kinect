   M S B A M L   `   `   `  ����  BodyBasics-WPF7    1Microsoft.Samples.Kinect.BodyBasics.ReachExercise   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Reach Exercise��5   	   $	��600��5   	   $	��700��6    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush��%�   &	        &	 #     &	 F     �� 5	   
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
   $��0��6   $��0 0 -1 0q�6!   $��Right=�63   $��Bottom=�6O   #��[  6j   $��Segoe UI)�6�   $��18(�6�   Reach Exercise6�   �� 5   
   $��0��6   $��Images\Status.png��6   $
��None=�68   $��Center=�6G   $��0 0 0 5q�6d   (� 5   
   $��1��6   $��Center=�6   .��5      �� $��UniformToFill=�63   ��6   �� *ImageSource+63   5      �� 5   
   -   5        $��	statusBar��5   ?   $��2��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   .@�5      �� ��6   �� *
StatusText+5      �� 5   
   -   5         ��Name$ User1_KinectID��5      $
��Left=�6S   $��0,20,0,0q�6n   $	��Top=�6�    J� RenderTransformOrigin$ -0.441,3.115&�6�   $��1��6�   ���	#FFCF55D4��6)   �� *UserKinectTrackingID1+��6�   Z� 6�   �� 5    
   -   5        $ User2_KinectID��5       $��Right=�6R   $��	0,20,-1,0q�6n   $ -0.441,3.115&�6�   $��1��6�   ���	#FFCF55D4$	��Top=�6�   ��6(   �� *UserKinectTrackingID2+��6�   Z� 6�   (� 5!   
   $��280,20,280,0q�6    (� Stretch$
 None=�6)   $��1��68   $��Center=�6E   $	��Top=�6b   $��89��6z   .��5"      �� -   5         ��Name$ Start_Reach��5"        btn-info#��[ 6*   $��Start Exercise��6L   $��Center=�6e   $��1��6�   $	��Top=�6�   6�   5#      5$      5%      