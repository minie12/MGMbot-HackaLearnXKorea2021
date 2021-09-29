# 🚘MGM - 운전면허시험 궁금증 해결 챗봇 서비스

![mgm-banner](Challenge/MGM-Banner.png)

- [HackaLearn x Korea 2021](https://github.com/devrel-kr/HackaLearn) 참가 작품
- 🏅[가장 창의적인 애플리케이션 부문 수상](https://github.com/devrel-kr/HackaLearn#-hackalearn-%EC%88%98%EC%83%81%EC%9E%90-%EB%B0%8F-%EC%88%98%EC%83%81%ED%8C%80)
- **Demo Page:** https://brave-tree-0cbae0d10.azurestaticapps.net/
- **Demo Video:** https://youtu.be/f7-GkbOhVGE


#### Index

- [개발 배경](#개발-배경)
- [사용 기술 및 개발 환경](#사용-기술--개발-환경)
- [로컬에서 실행하기](#로컬-환경에서-설치--실행)
- [Azure로 배포하기](#Azure-Portal로-배포하기)
- [팀 멤버](#Team)



# 🚘개발 배경

### ✍️왜 Azure Service를 썼을까요?

해커런에서 클라우드 스킬을 배우며 설정한 **Team: KING**의 개발 목표는 다음과 같습니다.

```
Azure Service를 활용하여 유용한 앱을 만들고,
Azure Static Web Apps를 통해 편리하게 배포해보자!
```

- `Azure Bot Service`를 이용해 운전면허 Q&A 챗봇 서비스를 개발했습니다.
- 개발 과정에서 `Microsoft Docs`의 문서와 `Sample GitHub Repository`를 적극 참고했습니다.
- 완성된 챗봇을 `Vue.js` 프로젝트에 삽입하여, 웹 환경에서 실행되는 챗봇을 구현했습니다.
- 마지막으로, 빌드 및 배포는 `Azure Static Web Apps와 GitHub Actions`를 통해 자동화했습니다.
- 즉, MGM 프로젝트는 이 저장소에 새 버전이 나올 때마다 웹으로 자동적으로 배포됩니다! 👏



### ✍️왜 운전면허시험 Q&A 서비스를 만들었나요?

- 팀원들이 운전면허를 취득하는 과정에서 다양한 궁금증이 발생했던 일화가 있었습니다.
- 국가시험이라 상황마다 필요한 준비물이나 합격 조건 등이 다양했기 때문입니다.
- 그렇기에, 사용자가 편히 쓸 수 있는 `챗봇`을 활용한 Q&A 서비스를 개발했습니다. 👍



### ✍️MGM 서비스는 어떤 특징이 있나요?

- 챗봇을 시작하면, 카테고리 형식으로 가장 중요한 정보를 출력합니다.
- 시험을 응시할 지역을 선택하면, 해당 지역의 시험장을 슬라이드 형식으로 출력합니다.
- 입력창에 질문을 쓰면, 적절한 대답을 출력합니다. 
- 예를 들어, 이런 식으로 질문해보세요! 🤗
  - 학과시험 수수료 얼마야?
  - 스쿠터 면허는 어떤 종류야?
  - 기능시험 실격 기준이 뭐야?



## 🚘사용 기술 & 개발 환경

#### 기술 스택

- [Azure Static Web Apps](https://aka.ms/hackalearn/aswa/intro)
- [Azure Bot Service](https://azure.microsoft.com/ko-kr/services/bot-services/)
- [Bot Framework v4 SDK Templates for Visual Studio](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4)
- [GitHub Actions](https://aka.ms/hackalearn/gha/intro)
- [C# .NET core](https://dotnet.microsoft.com/download?WT.mc_id=dotnet-33677)
- [Vue.js](https://cli.vuejs.org/)

#### 개발 환경

- [Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/vs/?WT.mc_id=dotnet-33677)
- [Visual Studio Code](https://code.visualstudio.com/?WT.mc_id=dotnet-33677)
- [Bot Framework emulator](https://github.com/Microsoft/BotFramework-Emulator)



## 🚘실행 방법

### 개발 도구 세팅 확인

프로젝트를 개발, 확인하기 위해 아래 항목의 설치가 필요합니다.

[HackaLearn에서 제공하는 개발 도구 리스트](https://github.com/devrel-kr/HackaLearn/tree/main/tools)를 참고해주세요.

- [애저 무료 계정](https://azure.microsoft.com/ko-kr/free/?WT.mc_id=dotnet-33677)
- [깃헙 무료 계정](https://github.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download?WT.mc_id=dotnet-33677)
- [Node.js](https://nodejs.org/ko/download/)
- [Vue CLI](https://cli.vuejs.org/)
- [nvm (Node Version Manager) for Windows](https://github.com/nvm-sh/nvm)
- [Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/vs/?WT.mc_id=dotnet-33677)
- [Visual Studio Code](https://code.visualstudio.com/?WT.mc_id=dotnet-33677)



## 🚘로컬 환경에서 설치 & 실행

#### 아래 사항이 준비되었는지 확인해주세요.

- **[MGM 프로젝트 저장소](https://github.com/solidcellaMoon/MGMbot-HackaLearnXKorea2021)를 `fork`한 Github Repository**

  `fork`한 저장소의 링크는 아래의 형식과 같습니다.

```
https://github.com/본인의 Github 아이디/MGMbot-HackaLearnXKorea2021
```

- **최신 버전의 [Node.js](https://nodejs.org/ko/download/)**

  cmd창에서 npm 명령어가 정상적으로 실행되어야 합니다.

---

### 프로젝트 받기 & 환경 설정

1. `git clone`으로 저장소를 받아옵니다.

```
git clone https://github.com/본인의 Github 아이디/MGMbot-HackaLearnXKorea2021.git
```

2. 저장소를 불러온 폴더로 이동합니다. 

`clone` 명령어를 실행한 폴더에 `MGMbot-HackaLearnXKorea2021` 라는 이름의 폴더가 생성될 것입니다.

```
cd MGMbot-HackaLearnXKorea2021
```

3. 실행, 빌드를 위해 `npm`을 설치합니다.

```
npm install
```



### 로컬에서 실행하기

저장소가 저장된 폴더에서 해당 명령어를 실행합니다.

```
npm run serve
```



## 🚘Azure Portal로 배포하기

[HowToDeployAzure.md](/HowToDeployAzure.md) 문서에서 확인할 수 있습니다. 😄



## 🚘Team

**[Team: KING](https://github.com/devrel-kr/HackaLearn/blob/main/teams/KING.md)**

- [이수민](https://github.com/vilut1002)
- [문지현](https://github.com/solidcellaMoon)
- [임은정](https://github.com/minie12)
