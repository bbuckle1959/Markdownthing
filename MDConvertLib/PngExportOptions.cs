namespace MDConvertLib
{
    public class PngExportOptions
    {
        /// <summary>Browser viewport width in CSS pixels (matches preview layout width).</summary>
        public int ViewportWidth { get; set; } = 1200;

        /// <summary>2x gives sharper images for READMEs and docs.</summary>
        public double DeviceScaleFactor { get; set; } = 2;
    }
}
