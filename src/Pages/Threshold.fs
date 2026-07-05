module Pages.Threshold

open Feliz

/// The Tree — landing room for the Komitas archive.
/// Reinterprets the original CD-ROM design: a knotted tree with a green canopy
/// of five sections (PHOTOS, VIDEO, AUDIO, TEXTS, CHRONOLOGY), Komitas reading
/// at the base, roots that read like Armenian handwriting.
///
/// Content comes from the CD's `data/text/{am,en,ru}/menu.xml`. Submenu labels
/// hand-typed here for the scaffold; a build-time XML → YAML step is planned
/// (see CLAUDE.md §12 Phase 2).

// ─── Section data ─────────────────────────────────────────────

type LabelPos = { X: float; Y: float }

type Section = {
    Id: string
    Label: string
    Sub: string list
    LabelPos: LabelPos
}

let sections : Section list = [
    { Id = "audio"
      Label = "AUDIO"
      LabelPos = { X = 700.0; Y = 130.0 }
      Sub = [
        "Love and Lyric Songs"; "Work Songs"; "Epic Songs"
        "Wanderer Songs"; "Patriotic Songs"; "Ceremonial Songs"
        "Songs from Akn"; "Piano Compositions"; "Spiritual Works"
        "Post-Komitasian Developments"; "Modern Arrangements"; "Radio Broadcasts"
      ] }
    { Id = "video"
      Label = "VIDEO"
      LabelPos = { X = 240.0; Y = 300.0 }
      Sub = [
        "TV Broadcasts"; "Concert Performances"; "Clips"; "YouTube"
      ] }
    { Id = "photos"
      Label = "PHOTOS"
      LabelPos = { X = 180.0; Y = 550.0 }
      Sub = [
        "Komitas"; "Documents"; "Covers of Printed Works"
        "Concert Posters"; "Visited Places"; "Contemporaries"; "Artwork"
      ] }
    { Id = "texts"
      Label = "TEXTS"
      LabelPos = { X = 1210.0; Y = 300.0 }
      Sub = [
        "Biography"; "Research"; "Letters"
        "Komitas Studies"; "Works Dedicated to Komitas"
      ] }
    { Id = "chronology"
      Label = "CHRONOLOGY"
      LabelPos = { X = 1220.0; Y = 550.0 }
      Sub = [
        "1869–1881 · Childhood in Kütahya"
        "1881–1895 · St. Echmiadzin"
        "1896–1899 · Studies in Berlin"
        "1899–1910 · Again in St. Echmiadzin"
        "1910–1916 · Constantinople"
        "1916–1935 · Years of Silence"
      ] }
]

// ─── SVG helpers ──────────────────────────────────────────────

let leafShape (id: string) (dPath: string) : ReactElement =
    Svg.path [
        svg.className (sprintf "leaf leaf-%s" id)
        svg.d dPath
    ]

// ─── The tree ─────────────────────────────────────────────────
// viewBox 0 0 1440 960 — hand-drawn approximation of the CD tree.

let treeSvg () : ReactElement =
    Svg.svg [
        svg.viewBox (0, 0, 1440, 960)
        svg.className "tree-svg"
        svg.custom ("preserveAspectRatio", "xMidYMid meet")
        svg.children [

            // ── Canopy leaves (five hover regions) ──
            leafShape "audio" "M 640 60 C 520 60 460 130 470 220 C 380 210 320 280 350 360 C 420 400 500 380 570 340 C 640 400 750 400 820 340 C 900 380 990 360 1030 300 C 1080 230 1030 150 940 130 C 900 60 780 40 720 90 C 690 65 670 60 640 60 Z"
            leafShape "video" "M 190 200 C 100 220 60 300 100 380 C 130 460 240 460 320 420 C 380 400 400 360 400 320 C 400 240 320 190 240 190 C 220 190 200 195 190 200 Z"
            leafShape "photos" "M 130 460 C 80 470 40 530 70 590 C 100 650 190 660 260 620 C 320 590 330 540 320 500 C 300 460 220 445 170 455 C 160 456 145 458 130 460 Z"
            leafShape "texts" "M 1210 220 C 1300 220 1360 300 1330 380 C 1290 460 1180 460 1110 420 C 1050 400 1030 360 1030 320 C 1030 240 1130 200 1210 220 Z"
            leafShape "chronology" "M 1230 460 C 1310 470 1360 540 1320 610 C 1280 670 1180 670 1120 630 C 1070 600 1060 550 1080 510 C 1110 470 1180 458 1230 460 Z"

            // ── Branches ──
            Svg.g [
                svg.className "branches"
                svg.children [
                    Svg.path [ svg.d "M 720 900 C 700 780 725 620 720 500 C 715 400 700 300 720 200 C 730 130 745 80 750 60" ]
                    Svg.path [ svg.d "M 720 400 C 690 380 640 360 580 340 C 510 320 440 270 420 220" ]
                    Svg.path [ svg.d "M 720 400 C 750 380 800 360 860 340 C 940 320 1010 270 1030 220" ]
                    Svg.path [ svg.d "M 720 320 C 680 300 610 260 520 200 C 460 170 400 140 340 130" ]
                    Svg.path [ svg.d "M 720 320 C 760 300 830 260 920 200 C 980 170 1040 140 1100 130" ]
                    Svg.path [ svg.d "M 720 460 C 620 470 490 480 380 500 C 330 510 280 530 250 560" ]
                    Svg.path [ svg.d "M 720 460 C 820 470 950 480 1060 500 C 1120 510 1170 530 1200 560" ]
                    Svg.path [ svg.d "M 460 240 C 470 220 490 210 510 220 C 500 230 480 240 460 240 Z" ]
                    Svg.path [ svg.d "M 950 240 C 940 220 920 210 900 220 C 910 230 930 240 950 240 Z" ]
                    Svg.path [ svg.d "M 620 220 C 640 200 670 200 690 220 C 660 230 630 230 620 220 Z" ]
                ]
            ]

            // ── The ground ──
            Svg.path [
                svg.className "earth"
                svg.d "M 200 720 C 320 700 520 690 720 700 C 920 690 1120 700 1240 720 C 1290 740 1300 820 1240 880 C 1120 900 920 910 720 900 C 520 910 320 900 200 880 C 140 820 150 740 200 720 Z"
            ]

            // ── Root text ──
            Svg.g [
                svg.className "root-text"
                svg.children [
                    Svg.path [ svg.d "M 260 760 q 30 -20 60 0 q 30 20 60 0 q 30 -20 60 0" ]
                    Svg.path [ svg.d "M 250 810 q 25 -15 50 0 q 25 15 50 0 q 25 -15 50 0" ]
                    Svg.path [ svg.d "M 260 860 q 30 -20 60 0 q 30 20 60 0" ]
                    Svg.path [ svg.d "M 950 780 q 30 -20 60 0 q 30 20 60 0 q 30 -20 60 0" ]
                    Svg.path [ svg.d "M 970 830 q 25 -15 50 0 q 25 15 50 0" ]
                    Svg.path [ svg.d "M 950 870 q 30 -20 60 0 q 30 20 60 0 q 30 -20 60 0" ]
                    Svg.path [ svg.d "M 320 890 q 15 -12 30 0 q 15 12 30 0" ]
                    Svg.path [ svg.d "M 1020 890 q 15 -12 30 0 q 15 12 30 0" ]
                ]
            ]

            // ── Komitas figure ──
            Svg.g [
                svg.className "komitas"
                svg.transform.translate(660, 640)
                svg.children [
                    Svg.path [
                        svg.className "robe"
                        svg.d "M 60 40 C 40 45 20 90 15 140 C 12 180 12 220 25 250 C 40 265 60 268 90 260 L 100 240 C 90 200 88 160 92 130 C 96 100 90 80 100 60 C 95 45 78 38 60 40 Z"
                    ]
                    Svg.circle [ svg.cx 55; svg.cy 20; svg.r 18; svg.className "head" ]
                    Svg.path [
                        svg.className "book"
                        svg.d "M 88 130 L 130 128 L 132 156 L 90 158 Z"
                    ]
                ]
            ]

            // ── Canopy labels as SVG text (scale-safe) ──
            Svg.g [
                svg.className "canopy-labels"
                svg.children [
                    for s in sections do
                        Svg.text [
                            svg.x s.LabelPos.X
                            svg.y s.LabelPos.Y
                            svg.className (sprintf "canopy-label label-%s" s.Id)
                            svg.custom ("text-anchor", "middle")
                            svg.custom ("dominant-baseline", "middle")
                            svg.text s.Label
                        ]
                ]
            ]
        ]
    ]

// ─── Right-column blocks ──────────────────────────────────────

let signatureBlock () : ReactElement =
    Html.div [
        prop.className "signature"
        prop.children [
            Html.div [
                prop.className "signature-name"
                prop.children [
                    Html.p [ prop.lang "hy"; prop.text "ԿՈՄԻՏԱՍ ՎԱՐԴԱՊԵՏ" ]
                    Html.p [ prop.lang "ru"; prop.text "КОМИТАС ВАРДАПЕТ" ]
                    Html.p [ prop.text "KOMITAS VARDAPET" ]
                ]
            ]
            Html.div [
                prop.className "signature-mark"
                prop.title "Komitas's signature (stylised — pending scan of the real hand)"
                prop.children [
                    Svg.svg [
                        svg.viewBox (0, 0, 240, 60)
                        svg.className "sig-svg"
                        svg.children [
                            Svg.path [ svg.d "M 10 40 C 25 15 45 55 60 35 C 75 15 90 45 110 30 C 130 15 145 45 165 30 C 180 20 200 40 215 30 L 230 50" ]
                        ]
                    ]
                ]
            ]
        ]
    ]

let languageSwitcher () : ReactElement =
    Html.nav [
        prop.className "lang-switch"
        prop.ariaLabel "Language"
        prop.children [
            Html.button [
                prop.className "lang-btn"
                prop.lang "hy"
                prop.text "-Հայերեն-"
                prop.type' "button"
            ]
            Html.button [
                prop.className "lang-btn active"
                prop.text "-English-"
                prop.type' "button"
            ]
            Html.button [
                prop.className "lang-btn"
                prop.lang "ru"
                prop.text "-Русский-"
                prop.type' "button"
            ]
        ]
    ]

let controlIcons () : ReactElement =
    Html.nav [
        prop.className "controls"
        prop.ariaLabel "Controls"
        prop.children [
            Html.button [ prop.className "ctrl"; prop.ariaLabel "Back";  prop.type' "button"; prop.text "←" ]
            Html.button [ prop.className "ctrl"; prop.ariaLabel "Sound"; prop.type' "button"; prop.text "◁" ]
            Html.button [ prop.className "ctrl"; prop.ariaLabel "Close"; prop.type' "button"; prop.text "✕" ]
        ]
    ]

let submenuPanel () : ReactElement =
    Html.aside [
        prop.className "submenu"
        prop.children [
            for s in sections do
                Html.div [
                    prop.className (sprintf "submenu-block submenu-%s" s.Id)
                    prop.children [
                        Html.h2 [
                            prop.className "submenu-title"
                            prop.text s.Label
                        ]
                        Html.ul [
                            prop.className "submenu-list"
                            prop.children [
                                for label in s.Sub do
                                    Html.li [ prop.text label ]
                            ]
                        ]
                    ]
                ]
        ]
    ]

// ─── The Threshold page ───────────────────────────────────────

let render () =
    Html.main [
        prop.className "threshold"
        prop.children [
            Html.div [
                prop.className "tree-stage"
                prop.children [ treeSvg () ]
            ]
            Html.div [
                prop.className "right-column"
                prop.children [
                    signatureBlock ()
                    submenuPanel ()
                    controlIcons ()
                ]
            ]
            languageSwitcher ()
        ]
    ]
