namespace ME.ECS {

    public static class WorldInitializer {

        public static DisposeStatic disposeStatic = new DisposeStatic();
        
        static WorldInitializer() {

            WorldStaticCallbacks.RegisterCallbacks(Init, Dispose);
            WorldStaticCallbacks.RegisterCallbacks(InitState, DisposeState, InitResetState);
            WorldStaticCallbacks.RegisterCallbacks(OnEntityDestroy);

        }

        public class DisposeStatic {
            ~DisposeStatic() {
                WorldStaticCallbacks.UnRegisterCallbacks(Init, Dispose);
                WorldStaticCallbacks.UnRegisterCallbacks(InitState, DisposeState, InitResetState);
                WorldStaticCallbacks.UnRegisterCallbacks(OnEntityDestroy);
            }
        }
        
        public static void Init(World world) {
            
        }

        public static void Dispose(World world) {
            
        }

        public static void InitState(State state) {
            
        }

        public static void InitResetState(State state) {

            state.pluginsStorage.Add(ref state.allocator, new TimersStorage());

        }

        public static void DisposeState(State state) {
            
        }

        public static void OnEntityDestroy(State state, in Entity entity) {

            state.pluginsStorage.Get<TimersStorage>(ref state.allocator, TimersStorage.key).OnEntityDestroy(ref state.allocator, in entity);

        }

    }

}