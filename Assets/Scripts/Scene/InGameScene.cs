using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InGameScene : GameScene
{
    public Chapter CurrentChapter { get; private set; }
    
    [SerializeField] private List<Chapter> chapterList;

    private Dictionary<int, Chapter> _chapters;
    private int                      _chapterToPlay;

    protected override IEnumerator OnPrepareScene()
    {
        _chapters = chapterList
            .ToDictionary(chapter => chapterList.IndexOf(chapter) + 1, chapter => chapter);
        
        // todo OutGameScene Sets Chapter To Play!
        _chapterToPlay = SaveLoad.LoadLastChapter();

        CurrentChapter = Instantiate(_chapters[_chapterToPlay]);
        
        CurrentChapter.Initialize();
        
        Dim.FadeOut(1f, 1f);
        
        yield break;
    }

    protected override void OnSceneStarted()
    {
        CurrentChapter.PlayChapter();
    }
    
    protected override void OnReleaseScene()
    {
        
    }
}