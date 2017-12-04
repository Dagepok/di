using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory painterFactory;
        private readonly Func<Random, DragonSettingsGenerator> generatorFactory;


        public DragonFractalAction(
            IDragonPainterFactory painterFactory, 
            Func<Random, DragonSettingsGenerator> generatorFactory)
        {
            this.painterFactory = painterFactory;
            this.generatorFactory = generatorFactory;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = generatorFactory(new Random()).Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            painterFactory.CreateDragonPainter(dragonSettings).Paint();
        }
    }

    public interface IDragonPainterFactory
    {
        DragonPainter CreateDragonPainter(DragonSettings settings);
    }
}