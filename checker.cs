using System;
using System.Diagnostics;

namespace Checker
{
    public class alertInfo {
        public static int[,] vitalLimits = new int[,] { { 70, 150 }, { 90, 100000 }, { 30, 95 } }; //2d array containing limits in order BPM, SPO2, respRate
        public enum vitalsNames { BPM, SPO2, RESPRATE };
    }

    public class Alert {
        public bool status;
        public string alertMessage;
    }

    class vitalCheck {
       public static Alert vitalIsOkay(int id, float value) {
            Alert obj = new Alert();

            if (value < alertInfo.vitalLimits[id, 0]) {
                obj.status = false;
                obj.alertMessage = "Alert !! vital value is lower than the lower limit !!!";
            } else if (value > alertInfo.vitalLimits[id, 1]) {
                obj.status = false;
                obj.alertMessage = "Alert !! vital value is higher than the higher limit !!!";
             } else {
                obj.status = true;
                obj.alertMessage = "Vital value is Normal !";
            }

            return obj;
        }
    }

    public class alertMessage {
        public static void alert(Alert result) {
            Console.WriteLine(result.alertMessage);
        }
    }

    class Program
    { 
        static void ExpectTrue(Alert result)
        {
            if (!(result.status))
            {
                Console.WriteLine("Expected true, but got false");
                Environment.Exit(1);
            }
        }
        static void ExpectFalse(Alert result)
        {
            if (result.status)
            {
                Console.WriteLine("Expected false, but got true");
                Environment.Exit(1);
            }
        }
        static int Main()
        {
            alertMessage.alert(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.BPM, 80));
            alertMessage.alert(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.BPM, 69));
            alertMessage.alert(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.SPO2, 70));
            alertMessage.alert(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.BPM, 80));

            ExpectTrue(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.BPM, 80));
            ExpectFalse(vitalCheck.vitalIsOkay((int)alertInfo.vitalsNames.BPM, 69));
            Console.WriteLine("All ok");
            return 0;
        }
    }
}
