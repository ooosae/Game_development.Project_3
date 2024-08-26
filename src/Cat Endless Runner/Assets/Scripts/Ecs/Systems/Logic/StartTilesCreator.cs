using Ecs.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Ecs.Systems.Logic
{
    public class StartTilesCreator : IEcsInitSystem
    {
        private const int CountStartTiles = 10;
        private const int CountEmptyTiles = 4;
        
        private EcsCustomInject<TileCreator> _tileCreator; 
        
        public void Init(IEcsSystems systems)
        {
            for (int i = 0; i < CountStartTiles; ++i)
            {
                if (i < CountEmptyTiles)
                {
                    _tileCreator.Value.SpawnTile(isEmpty: true);
                }
                else
                {
                    _tileCreator.Value.SpawnTile();
                }
            }
        }
    }
}