# AM-Technology-APP
MECHENG 700A/B (Project #24)
Introduction:
This is a part 4 project initiated in 2025 by Jasper Koid and Felix Clark within the Department of Mechanical and Mechatronic engineering at the University of Auckland. 
Significants thanks and acknowledgements directed towards Dr. Olaf Diegel for setting the premise of the project, and allowing freedom of modification and interpretation
to  allow the students to decide the direction of our final solution. 

Background:
Additive Manufacturing (AM) is a relatively new technology that supports the paradigm shift towards Industry 4.0 with the transformation of digital technology being applied
to the manufacturing industry. This drives motivation to the improvement of highly customisable products of low volume, contrary to regular manufacturing of high volume, low customisation.
As a result, AM is of unique interst to various industries. However, because it widely differs to regular Traditional Manufacturing (TM), there is a lack of foundational
knowledge to adjust to AM technology, and therefore lead to underutilisation or applied ineffectively.  This software tool aims to be integreated with Inventor using its respective
API platform for convenient access to assess CAD models with user-defined parameters and geometrical analysis to provide a quantitative indication of a part's suitability for AM.
The code functions with IPT files and accepts STEP files with some margin of error. Currently functions for Inventor 2025.
(Note) It does not work with assemblies. STL files once converted to STEP files can be assessed (requires a separate Inventor plugin), however significant computation time should be known proportional to the number
of triangles on the model. The geometrical analysis uses a heuristic focused design, meaning the numerous faces could lead to intense CPU usage and time. 

How to Download:
1.) From our Github page, please download the "Pt4ProjectAddin.dll" and "Autodesk.Pt4ProjectAddin.Inventor.addin" files. The .dll file contains the logic of the program while the .addin contains information for the setup.
2.) For local utilisation of the program, send the 2 files to AppData\Roaming\Autodesk\ApplicationPlugins, which could be further sent to a folder for tidiness if desired.
3.) Load up Inventor. Click on "Add-Ins" on the Tools ribbon panel leading to the "Add-In Manager". (add Image)
4.) Close and re-open Inventor with a CAD model of your choosing. 
5.) Click on the Tools panel and select AM Thinker.(Image)
6.) This opens the user form which you may fill out yourself and the contextual information related to the part. (image)
7.) Press the "Compute" button to reveal the results form. (image)

Hope you enjoy our plug-in!
- Jasper Koid