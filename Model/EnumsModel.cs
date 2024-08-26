using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EnumsModel
    {
        public enum showType
        {
            show,
            hide
        }
        public enum Sele
        {
            sale,
            notsale
        }
        public enum Direction
        {
            Empty,
            shomal,
            jonob,
            shargh,
            gharb
        }
        public enum LicenceValidation
        {
            valid,
            notvalid,
            expiered
        }
        public enum LicenceType
        {
            appGenerate,
            buyLicence,
            crackLicence
        }
        public enum LicenceUsable
        {
            use,
            notuse,
        }
    }
}
