#if FIXED_POINT_MATH
using ME.ECS.Mathematics;
using tfloat = sfloat;
#else
using Unity.Mathematics;
using tfloat = System.Single;
#endif

namespace ME.ECS {
    
    public static class WorldInitializer {

        public static DisposeStatic disposeStatic = new DisposeStatic();

        private static bool initialized = false;
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoad]
        private static class EditorInitializer {
            static EditorInitializer() => WorldInitializer.Initialize();
        }
        #endif

        [UnityEngine.RuntimeInitializeOnLoadMethodAttribute(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() {
            
            if (WorldInitializer.initialized == false) {
                
                WorldStaticCallbacks.RegisterCallbacks(InitResetState);
                WorldStaticCallbacks.RegisterCallbacks(OnEntityDestroy);
                WorldStaticCallbacks.RegisterCallbacks(OnWorldLifetimeStep);

                WorldInitializer.initialized = true;
            }
            
        }

        public class DisposeStatic {
            ~DisposeStatic() {
                WorldStaticCallbacks.UnRegisterCallbacks(InitResetState);
                WorldStaticCallbacks.UnRegisterCallbacks(OnEntityDestroy);
                WorldStaticCallbacks.UnRegisterCallbacks(OnWorldLifetimeStep);

                WorldInitializer.initialized = false;
            }
        }
        
        public static void InitResetState(State state) {

            state.pluginsStorage.GetOrCreate<TimersStorage>(ref state.allocator);

        }

        public static void OnEntityDestroy(State state, in Entity entity, bool destroyHierarchy) {

            state.pluginsStorage.GetOrCreate<TimersStorage>(ref state.allocator).OnEntityDestroy(ref state.allocator, in entity);

        }

        public static void OnWorldLifetimeStep(World world, ComponentLifetime step, tfloat deltaTime) {

            if (step == ComponentLifetime.NotifyAllSystemsBelow) {

                world.GetState().pluginsStorage.GetOrCreate<TimersStorage>(ref world.GetState().allocator).Update(ref world.GetState().allocator, deltaTime);

            }

        }

    }

}