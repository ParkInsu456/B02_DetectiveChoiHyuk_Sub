public interface ITrigger
{

    // 트리거를 활성화할 메서드
    public void TriggerOn(); // 트리거가 활성화되는 조건    

    // 트리거로 실행시킬 메서드
    public void Execute();  // 실행 조건은 실행을 호출할 스크립트에서 작성.


}