﻿namespace Assets.Source.Model
{
    public class GameMap
    {
        public readonly int width;
        public readonly int height;

        public GameTileset tileset;

        public readonly GameMapTileLayer terrainLayer;
        public readonly GameMapTileLayer constructionLayer;
        public readonly GameMapTileLayer aboveLayer;
        public readonly GameMapEntityLayer entityLayer;

        public GameMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            terrainLayer = new GameMapTileLayer(width, height);
            constructionLayer = new GameMapTileLayer(width, height);
            aboveLayer = new GameMapTileLayer(width, height);
        
            entityLayer = new GameMapEntityLayer();

            tileset = GameTileset.tilesetA;
        }
    }
}
