using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAssignment : MonoBehaviour
{

    #region Fields
    //list of active towers
    //inventory of units
    //reference to currently highlighted unit
    bool unitHighlighted;
	#endregion

	void Start()
    {
        //initialize variables
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (unitHighlighted)
            {
                //if what you've clicked on is a new unit, highlight it

                //if what you've clikced on is a tower, move the unit to a tower

            }
            else
            {
                //highlight the new unit
                
            }


        }



        //see if a unit is clicked on

        //if the unit has been selected highlight it
        //unhighlight the old unit
        
    }
}
