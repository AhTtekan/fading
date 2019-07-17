using UnityEngine;

public class Pointer : MonoBehaviour
{

    public GameObject Orb;
    public GameObject Tower;
    public GameObject OrbArrow, TempleArrow;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Temple
        if (Tower != null)
        {
            var tDist = Vector3.Distance(transform.position, Tower.transform.position);
            if (tDist < 3)
            {
                TempleArrow.gameObject.SetActive(false);
            }
            else
            {
                TempleArrow.gameObject.SetActive(true);
                var tTran = Tower.transform.position - transform.position;
                float angle = Mathf.Atan2(tTran.y, tTran.x) * Mathf.Rad2Deg;
                TempleArrow.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }
        if (Orb != null)
        {
            //Orb
            var oDist = Vector3.Distance(transform.position, Orb.transform.position);
            if (oDist < 3)
            {
                OrbArrow.gameObject.SetActive(false);
            }
            else
            {
                OrbArrow.gameObject.SetActive(true);
                var oTran = Orb.transform.position - transform.position;
                float angle = Mathf.Atan2(oTran.y, oTran.x) * Mathf.Rad2Deg;
                OrbArrow.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }
    }
}
