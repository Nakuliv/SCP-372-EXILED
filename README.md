# SCP-372-EXILED
Adds a SCP-372 to the game.
## Description

SCP-372 is invisible (unless it shoots, throws grenades, opens doors, opens items lockers or speaks) and it cannot hurt other SCPs.
```s_c_p372:
# Whether or not this plugin is enabled.
  is_enabled: true
  spawn_chance: 50
  max_s_c_p372_count: 1
  spawn_message:
  # The broadcast content
    content: >-
      <b>You have spawned as <color=red>SCP-372</color></b>

      <i>you are invisible (unless you shoot or speak), cooperate with SCPs</i>
    # The broadcast duration
    duration: 10
    # The broadcast type
    type: Normal
    # Indicates whether the broadcast should be shown or not
    show: true
  # how much health should SCP-372 have
  health: 150
