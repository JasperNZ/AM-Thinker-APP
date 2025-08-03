# AM-Technology-APP
MECHENG 700A/B (Project #24)


Instructions to use Github:<br>
1.) Download Github Desktop - a software created by Github that makes using software features much easier and accessible compared to hand typing the code for it  
2.) File --> Clone Repository and then select a folder (directory path) and the repository to clone  - Create a folder that essentially stores a local copy of the code you've created.  
3.) Select editor using options - This ensures we use Visual Studio instead of some of the automatic IDEs, such as VS code.  
4.) Add the files into the folder - Git is a separate software that should be downloaded automatically with Github Desktop, this is the free software that tracks changes to a code and decides how they are managed. Adding files should be detected on yourhub Desktop app (example of such changes is below)
![Screenshot 2025-05-02 031759](https://github.com/user-attachments/assets/888d07c1-5873-464c-a62b-c58c85bf9a4c)
5.) Update AM-Technology-APP - textbox to write a commit message, text that states a summarised explanation of changes made with the Description box intended to be more verbose.  
6.) Commit n file to main - tells Git to  finalise what changes should be noted in the local repository to be sent to the remote repository  
7.) Push to origin - located on the header bar, this finalises the files to be sent to the remote repository (origin) to be stored and shared among users.  
8.) Fetch/Pull from origin - you may see this when other users edit the code. This is the act of extracting the files from the remote repository. Pull completely replaces the local repository with the files relevant to the remote repository, e.g files not uploaded, specific to your device like a json path file, etc. Fetch is likely to be used when we need to merge changes where you may have made local changes and another user has already made progress and uploaded to the remote repository. This requires the changes to be merged as it could laed to potential conflict in logic and is resolved within the IDE.  
9.) Branches - This creates copies of the main code that allows divergence for when multiple users are editing the main code. Each individual can save their progress and later merge it together to keep their changes intact.


Code Implementation: <br>
- Using Inventor API and VB.NET: <br>
- 3 materials of Plastic, Ceramic, Metal. 6 AM technologies of MEX, MJT, BJT, VPP, DED, PBF. 18 combinations
- Class-based approach of utilising hiearchy of classes to represent the materials and technologies.
	-	 ENUMS for Materials, numbers and Technologies
	- 	Or create a visual clas of AM technology, the inherit it to each type, ie MEX metal, MJT plastic, etc.
		- Each class has 7 parameters, and maybe a scoring function?
- Other classes include a mainuserform to input answers, calculate score, Static weightings?
- 









Using VS and VB.NET: <br>
1.) Ensure Solution Explorer set to Solution view
2.) Load Project
3.) Add --> existing item 
4.) Enjoy debugging and class hierarchy


Useful file locations:<br>
To add the plugin to Inventor by including the .dll and .addin file for tidiness.
.\AppData\Roaming\Autodesk\ApplicationPlugins\Pt4ProjectAddin
