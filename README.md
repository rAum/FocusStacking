# FocusStacking
Simple and mini project done in a week after work, implementing tool for focus stacking. Aside of experimenting a bit with algorithm and code, it's purpose was also to learn how one can connect C++ with C#, by using C++CLI.

Focus stacking enables to merge sorted, multiple images that has focus on different depth to get one, in focus image. 
Application is quite fast (but not optimized much) and has some parameters to play with that enables to get different quality results. It also tries to estimate a grayscale depth map which can be used to make 3d gif.

The bug picture was taken from http://grail.cs.washington.edu/projects/photomontage/

This is how the program looks like:
![Tool preview image](preview.jpg)

Here are pictures of sample results - params were set to: sobel, maxWeight 20, edge 130
![Big image of bug](/sobel_maxWeight_20edge_130depth.png)
![Big depth image of bug](/sobel_maxWeight_20edge_130depth_depth.png)

3D gif for above images:
![3D gif reconstructed from bug images](/3d.gif)
