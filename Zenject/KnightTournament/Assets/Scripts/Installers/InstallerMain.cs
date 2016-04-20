using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;


namespace Knights
{
    class InstallerMain : MonoInstaller
    {

        public override void InstallBindings()
        {
            Debug.Log("InstallerMain : InstallBindings");

            Container.Install<AutoBindInstaller>();

            Container.Bind<ITickable>().ToSingle<RoundController>();
            Container.Bind<IInitializable>().ToSingle<RoundController>();
            Container.Bind<RoundController>().ToSingle();

            Container.Bind<ITickable>().ToSingle<GameController>();
            Container.Bind<IInitializable>().ToSingle<GameController>();
            Container.Bind<GameController>().ToSingle();

            Container.Bind<LevelHelper>().ToSingle();
        }

    }


}
