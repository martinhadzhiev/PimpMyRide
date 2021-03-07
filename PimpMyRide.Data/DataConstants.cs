namespace PimpMyRide.Data
{
    using System;

    public static class DataConstants
    {
        #region UserConstants

        public const int NameMinLength = 2;
        public const int NameMaxLength = 30;

        #endregion

        #region CarConstants

        public const int MakeMinLength = 2;
        public const int MakeMaxLength = 20;

        public const int ModelMinLength = 1;
        public const int ModelMaxLength = 15;

        public const int MinYear = 1970;
        public const int MaxYear = 2017;

        public const int MinPrice = 1;
        public const int MaxPrice = int.MaxValue;

        public const int PictureMaxLength = 2048 * 1000;

        #endregion

        #region PartConstants

        public const int PartMinLength = 2;
        public const int PartMaxLength = 20;

        public const int DescriptionMaxLength = 300;

        #endregion
    }
}
