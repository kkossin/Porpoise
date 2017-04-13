using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class gunTracker
    {
        public float reloadDelay=.5f;//time in seconds between reloads of a bullet
        public float fireDelay=.1f;//time in seconds between fires of a bullet

        public float reloadTimerLeft = 0;
        public float fireTimerLeft = 0;

        public float reloadTimerRight;
        public float fireTimerRight;

        public int gunAutoAmmoMax = 25;




        public int leftGunAutoAmmoCurrent;

        public int rightGunAutoAmmoCurrent;




        /// <summary>
        /// initialize gun system
        /// </summary>
        public gunTracker()
        {
            leftGunAutoAmmoCurrent = gunAutoAmmoMax;
            rightGunAutoAmmoCurrent = gunAutoAmmoMax;

            fireTimerLeft = fireDelay;
            fireTimerRight = fireDelay;
        }

        /// <summary>
        /// Update the state of the guns and progress their timers
        /// </summary>
        /// <param name="deltaTime"></param>
        public void update(float deltaTime)
        {
          
            fireTimerLeft += fireTimerLeft >= fireDelay ? 0 : deltaTime;
            fireTimerRight += fireTimerRight >= fireDelay ? 0 : deltaTime; ;

            reloadTimerLeft += reloadTimerLeft >= reloadDelay ? 0 : deltaTime;
            reloadTimerRight += reloadTimerRight >= reloadDelay ? 0 : deltaTime;


            if (reloadTimerLeft >= reloadDelay && leftGunAutoAmmoCurrent < gunAutoAmmoMax)//add another ammo if there is room and enough time has passed
            {
                leftGunAutoAmmoCurrent += 1;
                reloadTimerLeft = 0;
            }


            if (reloadTimerRight >= reloadDelay && rightGunAutoAmmoCurrent < gunAutoAmmoMax)//add another ammo if there is room and enough time has passed
            {
                rightGunAutoAmmoCurrent += 1;
                reloadTimerRight = 0;
            }
        }

        /// <summary>
        ///  Returns true if the gun denoted by left(vs right) can fire a shot
        /// 
        /// </summary>
        /// <param name="left">Checking the right or left gun</param>
        /// <param name="executeFire">Whether this check should update the state of the gun</param>
        /// <returns></returns>
        public bool canShoot(bool left, bool executeFire)
        {
            if (left)
            {
                if ((fireTimerLeft >= fireDelay) && (leftGunAutoAmmoCurrent > 0))
                    {
                    if (executeFire)
                    {
                        fireTimerLeft = 0;
                        leftGunAutoAmmoCurrent--;
                    }
                    return true;
                }

            }
            else
            {
                if ((fireTimerRight >= fireDelay) && (rightGunAutoAmmoCurrent > 0))
                {
                    if (executeFire)
                    {
                        fireTimerRight = 0;
                        rightGunAutoAmmoCurrent--;
                    }
                    return true;
                }
            }


            return false;
        }

    }
}
