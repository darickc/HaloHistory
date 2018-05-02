﻿using System;
//using System.Drawing;
using System.IO;

namespace HaloSharp.Model.Profile
{
    //[Serializable]
    public class GetImage : IEquatable<GetImage>
    {
        /// <summary>
        /// The URI the Image was retrieved from.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// The Image.
        /// </summary>
        public byte[] Image { get; set; }

        public bool Equals(GetImage other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            string firstBitmap;
            string secondBitmap;
            using (var memoryStream = new MemoryStream())
            {
                //Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                firstBitmap = Convert.ToBase64String(Image);

                memoryStream.Position = 0;

                //other.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                secondBitmap = Convert.ToBase64String(other.Image);
            }

            return string.Equals(Uri, other.Uri)
                && firstBitmap.Equals(secondBitmap);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof (GetImage))
            {
                return false;
            }

            return Equals((GetImage) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Image?.GetHashCode() ?? 0)*397) ^ (Uri?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(GetImage left, GetImage right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GetImage left, GetImage right)
        {
            return !Equals(left, right);
        }
    }
}
