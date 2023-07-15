# YunoCad

YunoCadは、Informatixが開発・提供するCADソフト、MicroGDSの.NET APIを取り扱うためのコンテキストベースのライブラリです。
このライブラリはMicroGDSのAPIをより安全で効率的に利用することを目指しています。

## 目的

MicroGDSのAPI、特に`Informatix.MGDS.Cad`クラスは、機能が文脈により異なるため、直接使うことが難しく、エラーを生じさせる可能性があります。
YunoCadはこれらのAPIを文脈に基づいてラップし、APIの使用をより安全で簡単にします。
これにより開発者はMicroGDSとのやりとりを文脈に応じて管理し、副作用をより良く制御できます。

## 主な機能

YunoCadは、主にMGDSNet.dll内の`Informatix.MGDS.Cad`クラスの静的メソッド群をより安全で使いやすくするために開発されています。YunoCadは文脈（Context）に基づくAPIの利用を提供します。現在の文脈は以下の階層構造を持っています：

- Global（通信がない状態）
  - Mgds（MicroGDSと通信中の状態）
    - Document（カレントのドキュメントが開かれているが、ビューは存在しない状態）
      - DrawingWindow（カレントのドローイングウィンドウが開かれている状態）
      - ElementsSelected（カレントのドキュメントが開かれており、一部の要素が選択されている状態）

## 貢献

フィードバック、バグレポート、改善提案などは、GitHubの[Issues](https://github.com/y121516/YunoCad/issues)や[Pull requests](https://github.com/y121516/YunoCad/pulls)を通じてお気軽にお寄せください。

## ライセンス

YunoCadはApache License 2.0のもとでライセンスされています。

## テスト

ソリューション内には`Informatix.MGDS.Cad`およびYunoCadの機能をテストするためのプロジェクトが組み込まれています。これらのテストを実行することで、YunoCadが正しく動作していることを確認できます。
