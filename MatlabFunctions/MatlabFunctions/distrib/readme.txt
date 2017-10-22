MATLAB Builder NE (.NET Component)


1. Prerequisites for Deployment 

. Verify the MATLAB Compiler Runtime (MCR) is installed and ensure you    
  have installed version 7.17 (R2012a).   

. If the MCR is not installed, do following:
  (1) enter
  
      >>mcrinstaller
      
      at MATLAB prompt. This MCR Installer command displays the 
      location of the MCR Installer.

  (2) run the MCR Installer.

Or download Windows 64bit version of MCR from the MathWorks website:

   http://www.mathworks.com/products/compiler/
   

For more information about the MCR and the MCR Installer, see 
“Working With the MCR” in the MATLAB Compiler User’s Guide.   
      
NOTE: You will need administrator rights to run MCRInstaller.

2. Files to Deploy and Package

-MatlabFunctions.dll
   -contains the generated component using MWArray API. 
-MatlabFunctionsNative.dll
   -contains the generated component using native API.
-This readme file

. If the target machine does not have version 7.17 of 
  the MCR installed, include MCRInstaller.exe.



Auto-generated Documentation Templates:

MWArray.xml - This file contains the code comments for the MWArray data conversion 
              classes and their methods. This file can be found in either the component 
              distrib directory or in
              <mcr_root>*\toolbox\dotnetbuilder\bin\win64\v2.0

MatlabFunctions_overview.html - HTML overview documentation file for the generated 
                                component. It contains the requirements for accessing the 
                                component and for generating arguments using the MWArray 
                                class hierarchy.

MatlabFunctions.xml - This file contains the code comments for the MatlabFunctions 
                                component classes and methods. Using a third party 
                                documentation tool, this file can be combined with either 
                                or both of the previous files to generate online 
                                documentation for the MatlabFunctions component.

                 


3. Resources

To learn more about:               See:
================================================================================================
The MWArray classes                MATLAB product help or <mcr_root>*\
                                   help\toolbox\dotnetbuilder\MWArrayAPI\MWArrayAPI.chm
Examples of .NET Web Applications  MATLAB Application Deployment 
                                   Web Example Guide


4. Definitions

For information on deployment terminology, go to 
http://www.mathworks.com/help. Select your product and see 
the Glossary in the User’s Guide.



* NOTE: <mcr_root> is the directory where MCR is installed on the target machine.

