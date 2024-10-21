/**
* Code generation. Don't modify! 
 */
using System.Runtime.CompilerServices;
using Atomic.AI;
using UnityEngine;
namespace Atomic.AI
{
    public static class BlackboardAPI
    {
        public const int Character = 1; // GameObject : class
        public const int Target = 2; // Transform : class
        public const int Waypoints = 3; // GameObject : class
        public const int TargetDistance = 4; // float
        public const int TreesService = 5; // GameObject : class
        public const int Conveyor = 6; // GameObject : class
        public const int Harvest = 7; // GameObject : class
        public const int HarvestingID = 8; // string


        ///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IBlackboard obj) => obj.HasObject(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetCharacter(this IBlackboard obj) => obj.GetObject<GameObject >(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IBlackboard obj, GameObject  value) => obj.SetObject(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IBlackboard obj) => obj.DelObject(Character);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IBlackboard obj) => obj.HasObject(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform  GetTarget(this IBlackboard obj) => obj.GetObject<Transform >(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IBlackboard obj, out Transform  value) => obj.TryGetObject(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IBlackboard obj, Transform  value) => obj.SetObject(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IBlackboard obj) => obj.DelObject(Target);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypoints(this IBlackboard obj) => obj.HasObject(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetWaypoints(this IBlackboard obj) => obj.GetObject<GameObject >(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypoints(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Waypoints, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypoints(this IBlackboard obj, GameObject  value) => obj.SetObject(Waypoints, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypoints(this IBlackboard obj) => obj.DelObject(Waypoints);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetDistance(this IBlackboard obj) => obj.HasFloat(TargetDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetTargetDistance(this IBlackboard obj) => obj.GetFloat(TargetDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTargetDistance(this IBlackboard obj, out float value) => obj.TryGetFloat(TargetDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTargetDistance(this IBlackboard obj, float value) => obj.SetFloat(TargetDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetDistance(this IBlackboard obj) => obj.DelFloat(TargetDistance);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTreesService(this IBlackboard obj) => obj.HasObject(TreesService);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetTreesService(this IBlackboard obj) => obj.GetObject<GameObject >(TreesService);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTreesService(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(TreesService, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTreesService(this IBlackboard obj, GameObject  value) => obj.SetObject(TreesService, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTreesService(this IBlackboard obj) => obj.DelObject(TreesService);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasConveyor(this IBlackboard obj) => obj.HasObject(Conveyor);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetConveyor(this IBlackboard obj) => obj.GetObject<GameObject >(Conveyor);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetConveyor(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Conveyor, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetConveyor(this IBlackboard obj, GameObject  value) => obj.SetObject(Conveyor, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelConveyor(this IBlackboard obj) => obj.DelObject(Conveyor);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasHarvest(this IBlackboard obj) => obj.HasObject(Harvest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetHarvest(this IBlackboard obj) => obj.GetObject<GameObject >(Harvest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetHarvest(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Harvest, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetHarvest(this IBlackboard obj, GameObject  value) => obj.SetObject(Harvest, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelHarvest(this IBlackboard obj) => obj.DelObject(Harvest);


    }
}
