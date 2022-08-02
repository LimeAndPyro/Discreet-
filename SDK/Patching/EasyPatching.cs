using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace Discreet.SDK.Patching
{
    //Created By: Lime/Pyro/Creed#1212 :)
    class PatchInstance
    {
        ///Change this to whatever (DiscreetInstance) and ("Discreet Patch") can both be changed However if changed must be changed in the func too!!
        public static HarmonyLib.Harmony DiscreetInstance = new HarmonyLib.Harmony("Discreet Patch");
        //HARMONY WILL ALWAYS NEED A INSTANCE TO PATCH!!!!

    }
    public class EasyPatching
    {
        //Patches into a specific property Such as float, int, etc.
        //EasyPatchPropertyPost Patches A Property After The Original Func is Ran
        public static void EasyPatchPropertyPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname) 
        {
            PatchInstance.DiscreetInstance.Patch(HarmonyLib.AccessTools.Property(inputclass, InputMethodName).GetMethod, null, new HarmonyLib.HarmonyMethod(outputclass, outputmethodname));
        }

        //EasyPatchPropertyPre Patches A Property Before The Original Func is Ran
        public static void EasyPatchPropertyPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            PatchInstance.DiscreetInstance.Patch(HarmonyLib.AccessTools.Property(inputclass, InputMethodName).GetMethod, new HarmonyLib.HarmonyMethod(outputclass, outputmethodname));
        }

        //patches into specific method For example class Deeznuts is the typeof(deeznuts) and the methodname lets say is "inyourmom".
        //EasyPatchMethodPre Patches a Method And its Func Before the Orig Func Is ran.
        public static void EasyPatchMethodPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            PatchInstance.DiscreetInstance.Patch(inputclass.GetMethod(InputMethodName), new HarmonyLib.HarmonyMethod(HarmonyLib.AccessTools.Method(outputclass, outputmethodname)));
        }
        //EasyPatchMethodPost Patches a Method And its Func After the Orig Func Is ran.
        public static void EasyPatchMethodPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            PatchInstance.DiscreetInstance.Patch(inputclass.GetMethod(InputMethodName), null, new HarmonyLib.HarmonyMethod(HarmonyLib.AccessTools.Method(outputclass, outputmethodname)));
        }
    }
}
        ///More Harmony INFO https://harmony.pardeike.net/api/HarmonyLib.html
        //------------------------------------------------------------------------------------------------------------------------------------------------------
        /*Property(Type, String)
        Gets the reflection information for a property by searching the type and all its super types

        Declaration
        public static PropertyInfo Property(Type type, string name)
        Parameters
        Type	Name	Description
        System.Type	type	
        The class/type

        System.String	name	
        The name

        Returns
        Type	Description
        System.Reflection.PropertyInfo	
        A property or null when type/name is null or when the property cannot be found

        -------------------------------------------------------------------------------------------------------------------------------------------------

        Method(Type, String, Type[], Type[])
        Gets the reflection information for a method by searching the type and all its super types

        Declaration
        public static MethodInfo Method(Type type, string name, Type[] parameters = null, Type[] generics = null)
        Parameters
        Type	Name	Description
        System.Type	type	
        The class/type where the method is declared

        System.String	name	
        The name of the method (case sensitive)

        System.Type[]	parameters	
        Optional parameters to target a specific overload of the method

        System.Type[]	generics	
        Optional list of types that define the generic version of the method

        Returns
        Type	Description
        System.Reflection.MethodInfo	
        A method or null when type/name is null or when the method cannot be found

        -----------------------------------------------------------------------------------------------------------------------------------------------
        Creating a Harmony instance
        Most patch operations require a Harmony instance. To instantiate Harmony, you simply call

        var harmony = new Harmony("com.company.project.product");
        The id should be in reverse domain notation and must be unique. In order to understand and react on existing patches of others, all patches in Harmony are bound to that id. This allows other authors to execute their patches before or after a specific patch by referring to this id.

        -=============================================================================================================================================================================================================================================================================================================

        You can unpatch every patch from an existing harmony instance or even all harmony patches all together:

        // every patch on every method ever patched (including others patches):
        var harmony = new Harmony("my.harmony.id");
        harmony.UnpatchAll();

        // only the patches that one specific Harmony instance did:
        harmony.UnpatchAll("their.harmony.id");*/


