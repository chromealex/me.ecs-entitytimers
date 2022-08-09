#if ENABLE_IL2CPP
#define INLINE_METHODS
#endif

#if FIXED_POINT_MATH
using ME.ECS.Mathematics;
using tfloat = sfloat;
#else
using Unity.Mathematics;
using tfloat = System.Single;
#endif

namespace ME.ECS {

    public static class WorldExt {

        #region TIMERS
        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static void SetTimer(this World world, in Entity entity, uint index, tfloat time) {

            E.IS_LOGIC_STEP(world);
            E.IS_ALIVE(in entity);

            ref var allocator = ref world.GetState().allocator;
            world.GetState().pluginsStorage.Get<TimersStorage>(ref allocator, TimersStorage.key).Set(ref allocator, in entity, index, time);

        }
        
        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ref tfloat GetTimer(this World world, in Entity entity, uint index) {

            E.IS_LOGIC_STEP(world);
            E.IS_ALIVE(in entity);

            ref var allocator = ref world.GetState().allocator;
            return ref world.GetState().pluginsStorage.Get<TimersStorage>(ref allocator, TimersStorage.key).Get(ref allocator, in entity, index);

        }
        
        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static tfloat ReadTimer(this World world, in Entity entity, uint index) {
            
            E.IS_ALIVE(in entity);

            ref var allocator = ref world.GetState().allocator;
            return world.GetState().pluginsStorage.Get<TimersStorage>(ref allocator, TimersStorage.key).Read(in allocator, in entity, index);

        }
        
        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static bool RemoveTimer(this World world, in Entity entity, uint index) {
            
            E.IS_LOGIC_STEP(world);
            E.IS_ALIVE(in entity);

            ref var allocator = ref world.GetState().allocator;
            return world.GetState().pluginsStorage.Get<TimersStorage>(ref allocator, TimersStorage.key).Remove(ref allocator, in entity, index);

        }
        #endregion

    }

    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public static class TimersEntityAPI {

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static Entity SetTimer(this in Entity entity, uint index, tfloat time) {

            Worlds.currentWorld.SetTimer(in entity, index, time);
            return entity;

        }

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static tfloat ReadTimer(this in Entity entity, uint index) {

            return Worlds.currentWorld.ReadTimer(in entity, index);

        }

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static ref tfloat GetTimer(this in Entity entity, uint index) {

            return ref Worlds.currentWorld.GetTimer(in entity, index);

        }

        #if INLINE_METHODS
        [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        #endif
        public static bool RemoveTimer(this in Entity entity, uint index) {

            return Worlds.currentWorld.RemoveTimer(in entity, index);

        }

    }

}