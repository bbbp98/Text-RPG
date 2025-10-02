## 프로젝트 소개
C#으로 만드는 Text Game입니다.


## 개발 기간
- 2025.09.30 ~ 2025.10.02


## 실행 화면
사진 클릭해서 영상 보러가기!
[![영상 보러가기](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG_ScreenShot.png)](https://youtu.be/r2sPghPYIhM)
https://youtu.be/r2sPghPYIhM


## 구현 목록
0. Main()
- bool타입 변수 isRunning이 참인 동안 무한 반복문을 돌립니다.
- 추상 클래스 Scene을 상속받는 여러 scene들에서 입력 값에 대한 동작을 수행합니다.

```cs
while (isRunning)
{
     character.Update();
     currentScene!.Show();
     Console.WriteLine();
     Console.WriteLine("원하시는 행동을 입력해주세요.");
     Console.Write(">> ");
     byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;  // check input, if TryParse return false => go to case default
     Console.Clear();
     currentScene.HandleInput(input);
}
```


-----------------------------------------------------------------------------

1. 캐릭터 정보 만들기
- Character 클래스를 만들어서 관리합니다.
- [Character.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Character.cs)

-----------------------------------------------------------------------------

2. 인벤토리
- Item들을 List로 관리합니다.
- Inventory Scene에서 foreach로 인벤토리의 아이템들을 출력합니다.

-----------------------------------------------------------------------------

3. 아이템 장착
- 아이템 옆에 표시된 숫자를 입력하면 캐릭터에게 아이템이 장착됩니다.
- 장착된 아이템은 '[E]' 문자가 표시됩니다.
- [EquipmentScene.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Scenes/EquipmentScene.cs)

-----------------------------------------------------------------------------

4. 랜덤 모험, 마을 순찰, 훈련하기
- 스태미너를 사용해서 골드, 경험치 등을 얻는 컨텐츠입니다.

-----------------------------------------------------------------------------

5. 상점
- 아이템을 사고 팔 수 있는 상점입니다.
- 아이템의 정보, 가격이 표시됩니다.
- 이미 구매를 완료한 아이템이라면 '구매완료'로 표시됩니다.
- 아이템 구매 시 보유 중인 골드가 적다면 '금액이 부족'하다는 텍스트를 출력합니다.
- 이미 보유 중인 아이템을 구매하면 '보유 중'이라는 텍스트를 출력합니다.
- 아이템 판매 시 장착 중인 아이템이라면 판매 후 장착 해제됩니다.
- [ShopScene.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Scenes/ShopScene.cs)
- [PurchaseItemScene.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Scenes/PurchaseItemScene.cs) 
- [SellingItemScene.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Scenes/SellingItemScene.cs)

-----------------------------------------------------------------------------

6. 던전
- 던전은 3가지 난이도가 있습니다.
- 난이도마다 권장 방어력이 있고, 권장 방어력보다 낮다면 40% 확률로 던전을 실패합니다.
- 던전을 클리어하면 골드와 경험치를 얻습니다.
- [DungeonClearScene.cs](https://github.com/bbbp98/Text-RPG/blob/main/TextRPG/Scenes/DungeonClearScene.cs)

-----------------------------------------------------------------------------

7. 휴식 기능
- 골드를 지불하면 체력과 스태미너를 회복할 수 있는 컨텐츠입니다.

-----------------------------------------------------------------------------

8. 레벨업 기능
- 레벨업에 필요한 경험치를 Character에서 배열로 관리합니다.
```cs
int beforeLevel = Level;
float beforeAtk = atk;
float beforeDef = def;

while (Exp > RequireExp[Level - 1])
{
     Exp -= RequireExp[Level - 1];
     Level++;
     atk += 0.5f;
     def += 1f;

     if (Exp > RequireExp[Level - 1])
          continue;

     Console.WriteLine("레벨업!!");
     Console.Write($"{"Level: ",-10}{beforeLevel,-5} => ");  // 이전 레벨
     Console.WriteLine(Level);
     Console.Write($"{"기본 공격력 : ",-10}{beforeAtk,-5} => ");  // 이전 공격력
     Console.WriteLine(atk);
     Console.Write($"{"기본 방어력 : ",-10}{beforeDef,-5} => ");  // 이전 방어력
     Console.WriteLine(def);
}
```

-----------------------------------------------------------------------------

9. 게임 저장하기 
- Start Scene에서 캐릭터의 정보를 저장할 수 있습니다.
- json을 활용하여 저장 기능을 구현했습니다.
```cs
// save player infomation
string saveJson = JsonSerializer.Serialize(character, new JsonSerializerOptions
{
     WriteIndented = true // JSON 보기 좋게 저장
});
File.WriteAllText(playerSaveData, saveJson);

// character initialize
if (File.Exists(playerSaveData))
{
     string json = File.ReadAllText(playerSaveData);
     character = JsonSerializer.Deserialize<Character>(json)!;
}
else
{
    Console.Write("이름을 입력하세요: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("직업을 입력하세요: ");
    string job = Console.ReadLine() ?? "";

    ...
}

```

## 