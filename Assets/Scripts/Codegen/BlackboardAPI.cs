/**
* Code generation. Don't modify! 
 */
using System.Runtime.CompilerServices;
using Atomic.AI;
using Unity.Mathematics;
using UnityEngine;
using Sample;
namespace Atomic.AI
{
    public static class BlackboardAPI
    {
        public const int TargetColliders = 1; // Collider[] : class
        public const int StoppingDistance = 2; // float
        public const int Character = 3; // GameObject : class
        public const int Waypoints = 4; // Transform[] : class
        public const int WaypointIndex = 5; // int
        public const int TargetObject = 6; // GameObject : class
        public const int AttackDistance = 7; // float
        public const int AttackTag = 8; // Tag
        public const int PatrolTag = 9; // Tag
        public const int FollowTag = 10; // Tag
        public const int SensorPosition = 11; // Transform : class


        ///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetColliders(this IBlackboard obj) => obj.HasObject(TargetColliders);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Collider[]  GetTargetColliders(this IBlackboard obj) => obj.GetObject<Collider[] >(TargetColliders);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTargetColliders(this IBlackboard obj, out Collider[]  value) => obj.TryGetObject(TargetColliders, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTargetColliders(this IBlackboard obj, Collider[]  value) => obj.SetObject(TargetColliders, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetColliders(this IBlackboard obj) => obj.DelObject(TargetColliders);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasStoppingDistance(this IBlackboard obj) => obj.HasFloat(StoppingDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetStoppingDistance(this IBlackboard obj) => obj.GetFloat(StoppingDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetStoppingDistance(this IBlackboard obj, out float value) => obj.TryGetFloat(StoppingDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetStoppingDistance(this IBlackboard obj, float value) => obj.SetFloat(StoppingDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelStoppingDistance(this IBlackboard obj) => obj.DelFloat(StoppingDistance);


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
		public static bool HasWaypoints(this IBlackboard obj) => obj.HasObject(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform[]  GetWaypoints(this IBlackboard obj) => obj.GetObject<Transform[] >(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypoints(this IBlackboard obj, out Transform[]  value) => obj.TryGetObject(Waypoints, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypoints(this IBlackboard obj, Transform[]  value) => obj.SetObject(Waypoints, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypoints(this IBlackboard obj) => obj.DelObject(Waypoints);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypointIndex(this IBlackboard obj) => obj.HasInt(WaypointIndex);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetWaypointIndex(this IBlackboard obj) => obj.GetInt(WaypointIndex);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypointIndex(this IBlackboard obj, out int value) => obj.TryGetInt(WaypointIndex, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypointIndex(this IBlackboard obj, int value) => obj.SetInt(WaypointIndex, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypointIndex(this IBlackboard obj) => obj.DelInt(WaypointIndex);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTargetObject(this IBlackboard obj) => obj.HasObject(TargetObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetTargetObject(this IBlackboard obj) => obj.GetObject<GameObject >(TargetObject);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTargetObject(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(TargetObject, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTargetObject(this IBlackboard obj, GameObject  value) => obj.SetObject(TargetObject, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTargetObject(this IBlackboard obj) => obj.DelObject(TargetObject);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAttackDistance(this IBlackboard obj) => obj.HasFloat(AttackDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetAttackDistance(this IBlackboard obj) => obj.GetFloat(AttackDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAttackDistance(this IBlackboard obj, out float value) => obj.TryGetFloat(AttackDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAttackDistance(this IBlackboard obj, float value) => obj.SetFloat(AttackDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAttackDistance(this IBlackboard obj) => obj.DelFloat(AttackDistance);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAttackTag(this IBlackboard obj) => obj.HasTag(AttackTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAttackTag(this IBlackboard obj) => obj.DelTag(AttackTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAttackTag(this IBlackboard obj) => obj.SetTag(AttackTag);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPatrolTag(this IBlackboard obj) => obj.HasTag(PatrolTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPatrolTag(this IBlackboard obj) => obj.DelTag(PatrolTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPatrolTag(this IBlackboard obj) => obj.SetTag(PatrolTag);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFollowTag(this IBlackboard obj) => obj.HasTag(FollowTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFollowTag(this IBlackboard obj) => obj.DelTag(FollowTag);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFollowTag(this IBlackboard obj) => obj.SetTag(FollowTag);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasSensorPosition(this IBlackboard obj) => obj.HasObject(SensorPosition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform  GetSensorPosition(this IBlackboard obj) => obj.GetObject<Transform >(SensorPosition);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetSensorPosition(this IBlackboard obj, out Transform  value) => obj.TryGetObject(SensorPosition, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetSensorPosition(this IBlackboard obj, Transform  value) => obj.SetObject(SensorPosition, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelSensorPosition(this IBlackboard obj) => obj.DelObject(SensorPosition);

    }
}
