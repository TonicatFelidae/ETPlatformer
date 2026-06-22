//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using ET.Engine;
//using ET.Shooterkit;
//using ET.SupportKit;
//using ET.SupportKit.Logic;
//using System;
//
//namespace ET.GO.Behaviour
//{
//    public class BH_shoot : Biiihaviour
//    {
//        protected IPoolManager _poolManager;
//        protected List<BarrelUnit> barrelUnits = new();
//        [Serializable]
//        public struct BarrelUnit
//        {
//            public Transform barrel;
//            public ProBarrel proBarrel;
//            public ETLogic_Filter shotDelayFilter;
//            public string bulletPath;
//        }
//        public override Biiihaviour Setup(GameObject gameObjectx)
//        {
//            base.Setup(gameObjectx);
//            return this;
//        }
//        public BH_shoot(IPoolManager poolManager)
//        {
//            _poolManager = poolManager;
//        }
//        public virtual void Construct(Transform barrel, ProBarrel proBarrel, string bulletPath)
//        {
//            BarrelUnit ret = new BarrelUnit();
//            ret.barrel = barrel;
//            ret.proBarrel = proBarrel;
//            ret.shotDelayFilter = new ETLogic_Filter(1f/proBarrel.RateOfFire);
//            ret.bulletPath = bulletPath;
//            barrelUnits.Add(ret);
//        }
//        public void Shoot(ControlType shootControlType)
//        {
//
//            switch (shootControlType)
//            {
//                case ControlType.FullAutomatic:
//                    Shoot_FullAutomatic();
//                    break;
//                case ControlType.Click:
//                    Shoot_Click();
//
//                    break;
//                default:
//                    break;
//            }
//        }
//        public virtual async void Shoot_Click()
//        {
//        }
//        public virtual void Shoot_FullAutomatic()
//        {
//        }
//        public void UpdateBulletDelayFilter()
//        {
//            barrelUnits[0].shotDelayFilter.UpdateTimeDodgeSignal();
//        }
//
//    }
//}

