using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace All_Windows_capslock_driver
{
    class driver_caps : IDisposable
    {
        bool Global = false; // apakah hook secara global untuk utilisasinya
        public delegate void LocalKeyEventHandler(Keys key, bool Shift, bool Ctrl, bool Alt);
        public event LocalKeyEventHandler KeyDown;
        public event LocalKeyEventHandler KeyUp;

        // callback nanti yang jadi referensi objek hook
        public delegate int CallbackEventAmbil(int Code, int W, int L);
        private int HookID = 0;
        CallbackEventAmbil cb_ambilkey = null; 


        ////import DLL//////////////////

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(HookType idHook, CallbackEventAmbil lpfn, int hInstance, int threadId);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetCurrentThreadId();


        //Type enum sesuai win API
        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        //Konstruktor Hook
        public driver_caps(bool global)
        {
            this.Global = global;
            cb_ambilkey = new CallbackEventAmbil(rekamEventHook);
            if (global)
            {
                //Hook secara global ke thread tunggu via dll low level
                HookID = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, cb_ambilkey, 0, 0); // ambil id hook di envi
                                                                                    
            }
            else
            {
                // Hook non global manggil event up dan down ke message (ruang lingkup aplikasi)
                HookID = SetWindowsHookEx(HookType.WH_KEYBOARD, cb_ambilkey,0, GetCurrentThreadId()); 
            }
        }



        //buat listener call back biar event key hook jalan
        private int rekamEventHook(int Code, int W, int L)
        {
            if (Code < 0)
            {
                return CallNextHookEx(HookID, Code, W, L);
            }
            try
            {                
                    KeyEvents kEvent = (KeyEvents)W;

                    // rekam 32 bit integer keycode low level 
                    Int32 keycodeRekamLowLevel = Marshal.ReadInt32((IntPtr)L);
                    //Console.WriteLine("code rekam---" + keycodeRekamLowLevel);
                    //Console.WriteLine(GetCapslock());

                    if (kEvent == KeyEvents.KeyDown || kEvent == KeyEvents.SKeyDown)
                    {
                        Console.WriteLine("MASUK DOWN");
                        //if (KeyDown != null)
                        //{
                            
                            if (keycodeRekamLowLevel == 20)
                            {
                                Console.WriteLine(GetCapslock());
                            }

                        //}
                    }
                    if (kEvent == KeyEvents.KeyUp || kEvent == KeyEvents.SKeyUp)
                    {
                        if (KeyUp != null)
                        {
                            // Event keyup yang tidak null fungsinya sesuai kedepannya
                        }
                    }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Ada error!\n Errornya itu-> " + e, "Aduhh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return CallNextHookEx(HookID, Code, W, L); // return callback eksekusi listen agar hook setelahnya
        }

        //hex sesuai winAPI terkait event
        public enum KeyEvents
        {
            KeyDown = 0x0100,
            KeyUp = 0x0101,
            SKeyDown = 0x0104,
            SKeyUp = 0x0105
        }

        [DllImport("user32.dll")] // import dll kusus state key biasa yang spesial buat shortcut dan sejenisnya seperti capslock ctrl
                                  // Deklarasi sesuai nama fungsinya
        static public extern short GetKeyState(System.Windows.Forms.Keys key_spesial);

        public static bool GetCapslock()
        {
            // mengambil state sebelumnya os windows trigger event langsung aktif
            if (Convert.ToBoolean(GetKeyState(System.Windows.Forms.Keys.CapsLock)) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // destruktor kelas probabbly dari garbage collector kemungkinan nanti 
        bool IsFinalized = false;
        ~driver_caps()
        {
            if (!IsFinalized)
            {
                UnhookWindowsHookEx(HookID);
                IsFinalized = true;
            }
        }
        public void Dispose()
        {
            if (!IsFinalized)
            {
                UnhookWindowsHookEx(HookID);
                IsFinalized = true;
            }
        }
    }
}
