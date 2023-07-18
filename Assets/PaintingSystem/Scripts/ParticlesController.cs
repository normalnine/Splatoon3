﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController: MonoBehaviour{
    public Color paintColor;
    
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //var pr = part.GetComponent<ParticleSystemRenderer>();
        //Color c = new Color(pr.material.color.r, pr.material.color.g, pr.material.color.b, .8f);
        //paintColor = c;
    }

    void OnParticleCollision(GameObject other) {
        //if(other.gameObject.name.Contains("Ground") && paintColor == new Color(0, 0.2877133f, 1))
        //{
        //    print("groundpaint");
        //    return;
        //}
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable p = other.GetComponentInChildren<Paintable>();//GetComponentInChildren<Paintable>();// GetComponent<Paintable>();
        if (p != null){
            for  (int i = 0; i< numCollisionEvents; i++){
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
                if (other.gameObject.name.Contains("Fist"))
                {
                   FistC.instance.bossFistHP--;
                }
                else if(other.gameObject.name.Contains("Hand") && HandC.instance.attackOK == true)
                {
                   HandC.instance.bossHandHP--;
                }
                else if (other.gameObject.name.Contains("Moon"))
                {
                    Moon1.instance.bossMoonHP--;
                }
                else if (other.gameObject.name.Contains("training"))
                {
                    other.gameObject.GetComponentInParent<KDH_Target>().Damaged();
                    
                }
            }
        }
    }
}