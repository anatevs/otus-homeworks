using UnityEngine;

namespace ShootEmUp
{
    public struct BulletArgs

    {
        public Vector2 position;

        public Vector2 velocity;

        public Color color;

        public int physicsLayer;

        public int damage;

        public bool isPlayer;

        public BulletArgs(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, bool isPlayer)
        {
            this.position = position;
            this.velocity = velocity;
            this.color = color;
            this.physicsLayer = physicsLayer;
            this.damage = damage;
            this.isPlayer = isPlayer;
        }
    }
}