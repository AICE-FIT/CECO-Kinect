   M S B A M L   `   `   `  ����I  EBodyBasics-WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Microsoft.Samples.Kinect.BodyBasics#Microsoft.Samples.Kinect.BodyBasics  5   	   6    0Microsoft.Samples.Kinect.BodyBasics.ReportWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Clocal1clr-namespace:Microsoft.Samples.Kinect.BodyBasics     5   	     � Title$  Report Window��5   	   $��405.042��6   $��630.251��60    � 	Resources 5	         exerciseListTemplate �� Path �� Orientation �� 
TargetType �� Triggers P� Property P� Value �� Property	 �� Value��%
  &	        '�� �    �� 5   
   .R�5      �� .+�5      �� $
��Bold"�6?   $��Consolas)�6Q   $��14.667(�6g   ��6   �� $ ExerciseName�6g   �� 5      $ 
Horizontal=�6   .+�5      �� $��Date: ��6    �� 5      ��6    �� $
 Date�5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Session ID: ��6    �� 5      ��6    �� $ 	SessionID�5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Employee ID: ��6    �� 5      ��6    �� $ 
EmployeeID�5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Hands: ��6    �� 5      ��6    �� $ Hands�5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Angle: ��6    �� 5       ��6    �� $ Angle�5!      �� 5"      $ 
Horizontal=�6   .+�5#      �� $��Time: ��6    �� 5$      ��6    �� $
 Time�5%      5&      5'      �� 5(   
   " ��6    5)      P� 5*       �@��AlternationIndex$ 0��6C   . �5+      ��  � ��	 �	#FF98FB986G   5,      P� 5-       �@��AlternationIndex$ 1��6C   . �5.      ��  � ��	 �	#FFADD8E663   5/      P� 50       �@��AlternationIndex$ 2��6C   . �51      ��  � ��	 �	#FFFA807263   52      54      55      .��56      (� .��57   
   � $��10,10,10,12q�6   
 � RowDefinitions
 58      �� 59      $	��23* �6    �� 5:      $	��67* �6    .+�5=      �� -   5         ��Name$ exerciseListBox��5=       �� AlternationCount$ 3��6/   $��Stretch=�6D   $	��203��6b   $��	10,18,0,0q�6o   $	��Top=�6�   $	��539��6�   #��[  5>      $��1��6G   �� 5?      -   5         ��Name$ patientNameLabel��5?      $��Patient Name:��6.   $
��Left=�6F   $��
173,58,0,0q�6a   $	��Top=�6u   $��89��6�   $��0��6�   �� 5@      -   5        $ patientIDLabel��5@      $��Patient ID: ��6,   $
��Left=�6C   $��
173,25,0,0q�6^   $	��Top=�6r   $��64��6�   $��0��6�   �� 5A      -   5        $ patientIDTextBox��5A      $
��Left=�6L   $��
242,25,0,0q�6g   $	��Top=�6{   ��6.   �� *	PatientID+6{   �� 5B      -   5        $ patientNameTextBox��5B      $
��Left=�61   $��
257,58,0,0q�6M   $	��Top=�6a   $��0��6y   ��5C      �� 5D      ��5E      `� 5F       �� StringFormat$ {0} {1}��6+   .1�5G   "   �� $ 	FirstName�6*   �� 5H   "   $ LastName�6*   5I      5K      5M      5N      5O      5P      