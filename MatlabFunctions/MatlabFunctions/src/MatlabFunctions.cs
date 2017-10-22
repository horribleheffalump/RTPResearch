/*
* MATLAB Compiler: 4.17 (R2012a)
* Date: Fri Oct 16 15:31:59 2015
* Arguments: "-B" "macro_default" "-W"
* "dotnet:MatlabFunctions,MatlabFunctions,0.0,private" "-T" "link:lib" "-d"
* "D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\MatlabFunctions\src" "-w"
* "enable:specified_file_mismatch" "-w" "enable:repeated_file" "-w"
* "enable:switch_ignored" "-w" "enable:missing_lib_sentinel" "-w" "enable:demo_license"
* "-v"
* "class{MatlabFunctions:D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\ExpFit.m,D
* :\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\GammaCDF.m,D:\Наука\projects\RTPRe
* search\CAPTools\MatlabFunctions\GammaFit.m,D:\Наука\projects\RTPResearch\CAPTools\Matlab
* Functions\GammaPDF.m}" 
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace MatlabFunctions
{

  /// <summary>
  /// The MatlabFunctions class provides a CLS compliant, MWArray interface to the
  /// M-functions contained in the files:
  /// <newpara></newpara>
  /// D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\ExpFit.m
  /// <newpara></newpara>
  /// D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\GammaCDF.m
  /// <newpara></newpara>
  /// D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\GammaFit.m
  /// <newpara></newpara>
  /// D:\Наука\projects\RTPResearch\CAPTools\MatlabFunctions\GammaPDF.m
  /// <newpara></newpara>
  /// deployprint.m
  /// <newpara></newpara>
  /// printdlg.m
  /// </summary>
  /// <remarks>
  /// @Version 0.0
  /// </remarks>
  public class MatlabFunctions : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Compiler Runtime
    /// instance.
    /// </summary>
    static MatlabFunctions()
    {
      if (MWMCR.MCRAppInitialized)
      {
        Assembly assembly= Assembly.GetExecutingAssembly();

        string ctfFilePath= assembly.Location;

        int lastDelimiter= ctfFilePath.LastIndexOf(@"\");

        ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

        string ctfFileName = "MatlabFunctions.ctf";

        Stream embeddedCtfStream = null;

        String[] resourceStrings = assembly.GetManifestResourceNames();

        foreach (String name in resourceStrings)
        {
          if (name.Contains(ctfFileName))
          {
            embeddedCtfStream = assembly.GetManifestResourceStream(name);
            break;
          }
        }
        mcr= new MWMCR("",
                       ctfFilePath, embeddedCtfStream, true);
      }
      else
      {
        throw new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the MatlabFunctions class.
    /// </summary>
    public MatlabFunctions()
    {
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~MatlabFunctions()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the ExpFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns exponential distribution parameters fitted by samples in X
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray ExpFit()
    {
      return mcr.EvaluateFunction("ExpFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the ExpFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns exponential distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="X">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray ExpFit(MWArray X)
    {
      return mcr.EvaluateFunction("ExpFit", X);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the ExpFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns exponential distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] ExpFit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "ExpFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the ExpFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns exponential distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="X">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] ExpFit(int numArgsOut, MWArray X)
    {
      return mcr.EvaluateFunction(numArgsOut, "ExpFit", X);
    }


    /// <summary>
    /// Provides an interface for the ExpFit function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// returns exponential distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void ExpFit(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("ExpFit", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaCDF()
    {
      return mcr.EvaluateFunction("GammaCDF", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaCDF(MWArray t)
    {
      return mcr.EvaluateFunction("GammaCDF", t);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaCDF(MWArray t, MWArray a)
    {
      return mcr.EvaluateFunction("GammaCDF", t, a);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <param name="b">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaCDF(MWArray t, MWArray a, MWArray b)
    {
      return mcr.EvaluateFunction("GammaCDF", t, a, b);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaCDF(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaCDF", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaCDF(int numArgsOut, MWArray t)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaCDF", t);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaCDF(int numArgsOut, MWArray t, MWArray a)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaCDF", t, a);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the GammaCDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <param name="b">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaCDF(int numArgsOut, MWArray t, MWArray a, MWArray b)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaCDF", t, a, b);
    }


    /// <summary>
    /// Provides an interface for the GammaCDF function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// returns value of Gamma CDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void GammaCDF(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("GammaCDF", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the GammaFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns Gamma distribution parameters fitted by samples in X
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaFit()
    {
      return mcr.EvaluateFunction("GammaFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the GammaFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns Gamma distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="X">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaFit(MWArray X)
    {
      return mcr.EvaluateFunction("GammaFit", X);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the GammaFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns Gamma distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaFit(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaFit", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the GammaFit M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns Gamma distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="X">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaFit(int numArgsOut, MWArray X)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaFit", X);
    }


    /// <summary>
    /// Provides an interface for the GammaFit function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// returns Gamma distribution parameters fitted by samples in X
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void GammaFit(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("GammaFit", numArgsOut, ref argsOut, argsIn);
    }


    /// <summary>
    /// Provides a single output, 0-input MWArrayinterface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaPDF()
    {
      return mcr.EvaluateFunction("GammaPDF", new MWArray[]{});
    }


    /// <summary>
    /// Provides a single output, 1-input MWArrayinterface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaPDF(MWArray t)
    {
      return mcr.EvaluateFunction("GammaPDF", t);
    }


    /// <summary>
    /// Provides a single output, 2-input MWArrayinterface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaPDF(MWArray t, MWArray a)
    {
      return mcr.EvaluateFunction("GammaPDF", t, a);
    }


    /// <summary>
    /// Provides a single output, 3-input MWArrayinterface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <param name="b">Input argument #3</param>
    /// <returns>An MWArray containing the first output argument.</returns>
    ///
    public MWArray GammaPDF(MWArray t, MWArray a, MWArray b)
    {
      return mcr.EvaluateFunction("GammaPDF", t, a, b);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaPDF(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaPDF", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaPDF(int numArgsOut, MWArray t)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaPDF", t);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaPDF(int numArgsOut, MWArray t, MWArray a)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaPDF", t, a);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the GammaPDF M-function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="t">Input argument #1</param>
    /// <param name="a">Input argument #2</param>
    /// <param name="b">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] GammaPDF(int numArgsOut, MWArray t, MWArray a, MWArray b)
    {
      return mcr.EvaluateFunction(numArgsOut, "GammaPDF", t, a, b);
    }


    /// <summary>
    /// Provides an interface for the GammaPDF function in which the input and output
    /// arguments are specified as an array of MWArrays.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// returns value of Gamma PDF with parameters a,b
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of MWArray output arguments</param>
    /// <param name= "argsIn">Array of MWArray input arguments</param>
    ///
    public void GammaPDF(int numArgsOut, ref MWArray[] argsOut, MWArray[] argsIn)
    {
      mcr.EvaluateFunction("GammaPDF", numArgsOut, ref argsOut, argsIn);
    }



    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
