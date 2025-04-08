using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Pile currentPile;
    public Pile targetPile;
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit))
            {
              
                
                if (hit.collider.gameObject.GetComponent<Pile>() != null)
                {
                    if(currentPile == null)
                    {
                        currentPile = hit.collider.gameObject.GetComponent<Pile>();
                        currentPile.GetNut().MoveNut(currentPile.firstPost);
                    }
                    else
                    {
                        if(currentPile == hit.collider.gameObject.GetComponent<Pile>())
                        {
                            currentPile.GetNut().transform.DOMove(currentPile.firstPost.transform.position, 0.3f).OnComplete(delegate {
                                currentPile = null;
                            });
                            
                            
                        }
                        else
                        {
                            if (hit.collider.gameObject.GetComponent<Pile>().GetNut() == null)
                            {
                                //var temp = currentPile.GetNut();
                                //var temp2 = hit.collider.gameObject.GetComponent<Pile>().piles[0].post;
                                //temp.transform.DOMove(currentPile.firstPost.transform.position, 0.5f).OnComplete(delegate
                                //{
                                //    temp.transform.DOMove(hit.collider.gameObject.GetComponent<Pile>().firstPost.transform.position, 0.5f).OnComplete(delegate
                                //    {
                                //        temp.transform.DOMove(temp2.transform.position, 0.5f).OnComplete(delegate
                                //        {



                                //        });
                                //    });
                                //});
                                //hit.collider.gameObject.GetComponent<Pile>().AddNut(currentPile.GetNut());
                                //currentPile.RemoveNut();
                                StartCoroutine(MoveStackOfNuts(currentPile, hit.collider.gameObject.GetComponent<Pile>()));
                                currentPile = null;
                                return;
                            }
                            if (currentPile.GetNut().id == hit.collider.gameObject.GetComponent<Pile>().GetNut().id)
                            {
                                
                                    //var temp = currentPile.GetNut();
                                    //var temp2 = hit.collider.gameObject.GetComponent<Pile>();
                                    //var temp3 = temp2.GetTargetPile().post;
                                    //temp.transform.DOMove(currentPile.firstPost.transform.position, 0.5f).OnComplete(delegate
                                    //{
                                    //    temp.transform.DOMove(temp2.firstPost.transform.position, 0.5f).OnComplete(delegate
                                    //    {
                                    //        temp.transform.DOMove(temp3.transform.position, 0.5f).OnComplete(delegate
                                    //        {


                                    //        });
                                    //    });
                                    //});
                                    //hit.collider.gameObject.GetComponent<Pile>().AddNut(currentPile.GetNut());
                                    //currentPile.RemoveNut();
                                
                                StartCoroutine(MoveStackOfNuts(currentPile, hit.collider.gameObject.GetComponent<Pile>()));
                                currentPile = null;
                            }
                        }
                    }
            
                  
                }
             


            }
        }
    }

    private IEnumerator MoveNutToTarget(Nuts nut, Pile fromPile, Pile toPile)
    {
        var temp = nut;
        var temp3 = toPile.GetTargetPile();

        if (temp3 == null) yield break; // check an toàn

        // Step 1: Move to fromPile.firstPost
        yield return temp.transform.DOMove(fromPile.firstPost.position, 0.3f).WaitForCompletion();

        // Step 2: Move to toPile.firstPost
        yield return temp.transform.DOMove(toPile.firstPost.position, 0.3f).WaitForCompletion();

        // Step 3: Move to final target position
        yield return temp.transform.DOMove(temp3.post.position, 0.3f).WaitForCompletion();

        // After all animations done, update data
        toPile.AddNut(nut);
        fromPile.RemoveNut();
    }
    private IEnumerator MoveStackOfNuts(Pile fromPile, Pile toPile)
    {
        var fromNutList = new List<Nuts>(fromPile.nutList); 
        var topNut = fromPile.GetNut();

        if (topNut == null)
            yield break;

        int matchingId = topNut.id;

        for (int i = fromNutList.Count - 1; i >= 0; i--)
        {
            if (fromNutList[i].id == matchingId)
            {
                if (toPile.GetTargetPile() == null)
                    break;

                yield return StartCoroutine(MoveNutToTarget(fromNutList[i], fromPile, toPile));
            }
            else
            {
                break;
            }
        }

        currentPile = null;
    }



}
