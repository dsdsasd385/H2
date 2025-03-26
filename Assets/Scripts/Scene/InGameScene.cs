using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InGameScene : GameScene
{
    [SerializeField] private List<Chapter> chapterList;

    private Dictionary<int, Chapter> _chapters;
    private int                      _lastPlayedChapter;

    protected override IEnumerator OnPrepareScene()
    {
        _chapters = chapterList
            .ToDictionary(chapter => chapterList.IndexOf(chapter) + 1, chapter => chapter);

        _lastPlayedChapter = SaveLoad.LoadLastChapter();
        
        yield break;
    }

    protected override void OnSceneStarted()
    {
        _chapters[_lastPlayedChapter].PlayChapter();
    }
    
    protected override void OnReleaseScene()
    {
        
    }
}