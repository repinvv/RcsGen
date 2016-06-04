namespace RcsGen
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell.Interop;

    [Guid("D03CB851-1B73-4447-9DD7-26FD3CFF03A9")]
    public class RcsGenerator : IVsSingleFileGenerator
    {
        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".generated.cs";
            return VSConstants.S_OK;
        }

        public int Generate(string wszInputFilePath, 
            string bstrInputFileContents, 
            string wszDefaultNamespace, 
            IntPtr[] rgbOutputFileContents, 
            out uint pcbOutput, 
            IVsGeneratorProgress pGenerateProgress)
        {
            // The contract between IVsSingleFileGenerator implementors and consumers is that 
            // any output returned from IVsSingleFileGenerator.Generate() is returned through  
            // memory allocated via CoTaskMemAlloc(). Therefore, we have to convert the 
            // byte[] array returned from GenerateCode() into an unmanaged blob.  
            var output = new Generator().Generate(bstrInputFileContents, wszDefaultNamespace, wszInputFilePath);
            var bytes = Encoding.ASCII.GetBytes(output);
            int outputLength = bytes.Length;
            rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(outputLength);
            Marshal.Copy(bytes, 0, rgbOutputFileContents[0], outputLength);
            pcbOutput = (uint)outputLength;
            return VSConstants.S_OK;
        }
    }
}
