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


TODO list:
URGENT:
- create helper functions in a separate file/class (geometry stuff like part complexity and overhang)
- pure functionalities, putting the right numbers for all combinations. Scores and weighting modifications
- Might replace simple WSM --> WA --> TOPSIS.
- report about future-proofing and considerations for advancements
- improve coding comments and consistent coding conventions to unionize the code
- fix scores for lead time - instead of lead time significance.
- Might add a special comment property for each AM profile. Incompatible profiles can then be noted
- Double check geometry features output correct numbers and units
- Consider if we can do this for multiple parts, STL files and assemblies.
- Post Processing Effort Calculator



UNLIKELY TO COMPLETE:
- testing testing (missing data)
- Temporary messagebox to inform user of selecting base face to print. (need to test Felix function - unusually buggy, might replace with own function)
- icon as tooltips - not feasible because lack of 16x16 question mark professional icons, might need to use SystemIcons
- shifting tooltip comments away from interfering drop down - currently can only be done manually using Show.

NICE TO HAVE:
- other nice design features for user 
- The user form feels suffocating because of how close the dropdown menus are, low priority to space them out.

COMPLETED:
- make bug fixing and error handler (ErrorHandler.vb) DONE
- improve the UI/UX DONE
- Considering adding a little icon next to the AM thinker? DONE
- Apply tooltips to all relevant sections DONE
- Preferably apply group boxes in the mainUserForm Designer form. for easier readability (fixed bugs in error handler from this) DONE
- fix the select profile loop (trying to use tuples, but it's not working???????)
- Big Improvement are using ENUMS for security, but dropdownlist are strings, so don't mind it. constrained dropdown menu already mitigates this issue anyways DONE
- Fix messages on tooltips to be more useful and not in the way of user selection options. Manually shifted below the mouse. DONE
- Final message box and interpretation. Layout is done, just needs everything else to come together DONE.
- score, traffic color, and feedback/comment file (helper message handler file). Layout finished, needs finalisation. DONE
- Fix dictionary to look through valid machine selection. goes through second groupings. DONE
- Developer mode for copying pasting data. DONE
- Fix looping dictionary for errors to be in proper order of visual selection. DONE




Backup notes on code:
- Think GeoHelper on-part bug of potentially reading wrong Parts is fixed, because it activates on compute button press!
Surprisingly the form follows the broswer and is not stationary relative to the window, compared to other built in feature windows like fillet or measure
should the result form be as wide as the input form
double check the up and down arrows (unicode) on details button don't crash the system
ask about visiblity and readability of the colours.
Preference of UI text?
