문서정보 : 2023.05.09.~ 작성, 작성자 [@SAgiKPJH](https://github.com/SAgiKPJH)

# Test_CSharp_CICD
C#으로 구성한 sln을 CI/CD를 통해 자동 빌드 및 테스트 하여 배포하는 테스트를 만든다.

### 목표
- [x] : 1. 목표 선정
- [ ] : 2. 프로젝트 구성
- [ ] : 3. CICD 구성
- [ ] : 4. Build & Deploy Test

### 제작자
[@SAgiKPJH](https://github.com/SAgiKPJH)

### 참조

- [Learn Git Hub CI/CD](https://github.com/SagiK-Repository/Learn-Git-Hub-CICD)

<br>

---

<br>

# 1. 목표 선정  

- 다음 조건을 만족한다.  

1. Visual Studio를 통해 C# sln 파일을 만든다. 
2. C# Test 프로젝트를 구성한다.
3. 특정 브랜치에 Commit 될 때 CICD가 동작한다.
4. CICD를 통해 자동 빌드 및 Test를 진행한다.
5. 문제가 없으면 자동 배포된다.

### 다음과 같은 흐름을 구성합니다.

1. 기본적인 빌드 Test 후 Release
   - Main 브랜치가 존재합니다.
   - Main 브랜치에 "Release v0.0.0"으로 특정 규칙에 맞도록 Commit을 합니다.
   - Main Repository 안에 존재하는 sln파일을 빌드합니다.
   - Unit Test Project도 Test 합니다.
   - 결과 문제 없으면 기입한 Tag에 맞춰서 Release를 진행합니다.
2. 실제 상황을 토대로 구성한 Release
   - Main 브랜치와 Develop 브랜치가 존재합니다.
     - Main 브랜치는 Release 전용이고, Develop 브랜치는 개발 전용입니다.
   - Develop에서 개발 후 "Relese v0.0.0"으로 특정 규칙에 맞도록 Commit 합니다.
   - Develop Repository 안에 존재하는 sln파일을 빌드합니다.
   - Unit Test Project도 Test 합니다.
   - 결과 문제 없으면 Main 브랜치에 정해진 특정 파일(dll, exe 파일 따위)를 Merge합니다.

* 다양하게 CICD를 구성해볼 수 있는데, 여기서는 1번 방법을 활용해봅니다.
