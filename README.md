# AM-Technology-APP
## MECHENG 700A/B (Project #24)
### Introduction: <br>
This is a part 4 project undertaken in 2025 by Jasper Koid and Felix Clark within the Department of Mechanical and Mechatronic engineering at the University of Auckland. <br>
Significants thanks and acknowledgements directed towards Dr. Olaf Diegel for setting the premise of the project, and allowing freedom of modification and interpretation to  allow the students to decide the direction of our final solution. Without his expertise and guidance, this project would have had a much more significantly difficult challenge in getting started while completed within the deadline.

### Background: <br>
Additive Manufacturing (AM) is a relatively new technology that supports the paradigm shift towards Industry 4.0 with the transformation of digital technology being applied to the manufacturing industry. This drives motivation to the improvement of highly customisable products of low volume, contrary to regular manufacturing of high volume, low customisation. As a result, AM is of unique interst to various industries. However, because it widely differs to regular Traditional Manufacturing (TM) there is a lack of foundational knowledge to adjust to AM technology. This results in AM being underutilised or applied ineffectively. To address this gap, this software tool aims to be integreated with Inventor using its respectiveAPI platform for convenient access to assess CAD models with user-defined parameters and geometrical analysis to provide a quantitative indication of a part's suitability for AM.

The code functions with IPT files and accepts STEP files with some margin of error. Currently functions for Inventor 2025.
(Note) It does not work with assemblies. STL files once converted to STEP files can be assessed (requires a separate Inventor plugin), however significant computation time should be known proportional to the number of triangles on the model. The geometrical analysis uses a heuristic-based design, meaning the numerous faces could lead to intense CPU usage and time. 

The basics of the code utilised the Analytical Hierarchy Process (AHP) and Weighted Sum Model (WSM) to provide a multi-criteria decision-making (MCDM) approach to assess the suitability of AM for a given part. The user is required to input various parameters related to the part and its manufacturing context, which are then processed alongside geometrical analysis of the CAD model to produce a final score indicating the suitability of AM.

How to Download: <br>
1.) From our Github page, please download the "Pt4ProjectAddin.dll" and "Autodesk.Pt4ProjectAddin.Inventor.addin" files. The .dll file contains the logic of the program while the .addin contains information for the setup.<br>
2.) For local utilisation of the program, send the 2 files to AppData\Roaming\Autodesk\ApplicationPlugins, which could be further sent to a folder for tidiness if desired.<br>
3.) Load up Inventor. Click on "Add-Ins" on the Tools ribbon panel leading to the "Add-In Manager". <br>
<img width="419" height="652" alt="Screenshot 2025-10-12 025614" src="https://github.com/user-attachments/assets/adc6a187-bdf9-4329-90d2-4d7767adc3a9" /> <br>
4.) Ensure the "Load Automatically" and "Loaded/Unloaded" is ticked. Close and re-open Inventor with a CAD model of your choosing. <br>
5.) Click on the Tools panel and select AM Thinker. <br>
<img width="1221" height="972" alt="Screenshot 2025-10-12 030127" src="https://github.com/user-attachments/assets/ae97609c-4f9f-446f-8fb2-50a4d7c77b8b" /> <br>
6.) This opens the user form which you may fill out yourself and the contextual information related to the part.  <br>
<img width="1915" height="985" alt="Screenshot 2025-10-12 030308" src="https://github.com/user-attachments/assets/147d7bce-00ee-4664-bdc0-e76c8a79b6c9" /> <br>
7.) Press the "Compute" button to reveal the results form. <br>
<img width="1916" height="987" alt="Screenshot 2025-10-12 030859" src="https://github.com/user-attachments/assets/212c8425-6df7-4f93-a669-0213168d6fd0" /> <br>

Hope you enjoy our plug-in!
\- Jasper Koid
