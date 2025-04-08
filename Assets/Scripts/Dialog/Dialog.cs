using System.Collections.Generic;

public static class Dialog
{
    private static Dictionary<int, string> _dialogList = new()
    {
        { 1001, "잠시 여유가 생겨 <color=#2989FF>체력 단련</color>을 했습니다!\n<color=#FF6666>최대 체력</color>이 <color=#04BD44>{0}% 증가</color>했습니다!"},
        { 1002, "다음 전투를 위해 무기의 날을 <color=#2989FF>연마</color>했습니다!\n<color=#FF9900>공격력</color>이 <color=#04BD44>{0}% 증가</color>했습니다!"},
        { 1003, "<color=#2989FF>더러워진 방어구를 닦아내었습니다.</color>\n반짝반짝 광이 나네요!\n<color=#66CCFF>방어력</color>이 <color=#04BD44>{0}% 증가</color>했습니다!"},
        { 1004, "<color=#2989FF>길가에 동전이 떨어져있네요!</color>\n주머니가 두둑해졌습니다.\n<color=#FFEA00>소지금</color>이 <color=#04BD44>{0} 증가</color>했습니다!"},
        { 1005, "<color=#2989FF>처음 보는 책을 발견했어요.</color>\n한번 읽어볼까요?\n<color=#32A852>경험치</color>가 <color=#04BD44>{0} 증가</color>했습니다!"},
        { 1006, "<color=#2989FF>처음보는 벌레에게 물렸어요.</color>\n화끈화끈 아파옵니다!\n<color=#FF6666>최대 체력</color>이 <color=#FF3419>{0}% 감소</color>했습니다!"},
        { 1007, "<color=#2989FF>무기를 연마하다가 부러지고 말았어요.</color>\n대장장이에게 혼나겠어요!\n<color=#FF9900>공격력</color>이 <color=#FF3419>{0}% 감소</color>했습니다!"},
        { 1008, "<color=#2989FF>살이 빠졌나? 방어구가 느슨해요.</color>\n벨트가 헐렁거립니다!\n<color=#66CCFF>방어력</color>이 <color=#FF3419>{0}% 감소</color>했습니다!"},
        { 1009, "<color=#2989FF>주머니에 구멍을 발견했어요.</color>\n동전 몇개를 떨어뜨린거같아요!\n<color=#FFEA00>소지금</color>이 <color=#FF3419>{0} 감소</color>했습니다!"},
    };

    public static string Get(int dialogCode) => _dialogList[dialogCode];
}