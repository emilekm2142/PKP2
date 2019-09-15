package yeah.hack.filizanka.controller;

import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import yeah.hack.filizanka.controller.dto.UserDto;
import yeah.hack.filizanka.service.UserService;

@RestController
@RequestMapping("/user")
@RequiredArgsConstructor
public class UserController {

    private final UserService userService;

    @GetMapping("/{id}")
    public UserDto getUserById(@PathVariable(value = "id") Long id) {
        return UserDto.from(userService.getUserById(id));
    }

    @PutMapping("{id}/train-ride/{train-ride-id}")
    public UserDto updateCurrentTrainRide(@PathVariable(value = "id") Long id,
                                          @PathVariable(value = "train-ride-id") String trainRideId,
                                          @RequestParam(value = "start", required = false) Long startingStationId,
                                          @RequestParam(value = "destination", required = false) Long destiantionId) {
        return UserDto.from(userService.updateCurrentTrainRide(id, trainRideId, startingStationId, destiantionId));
    }

    @PutMapping("{id}/skin/{skin-id}")
    public UserDto updateUserActiveSkin(@PathVariable(value = "id") Long id,
                                        @PathVariable(value = "skin-id") Long skinId) {
        return UserDto.from(userService.updateActiveSkin(id, skinId));
    }

    @PutMapping("{id}/locate")
    public UserDto locateUser(@PathVariable(value = "id") Long id, @RequestParam(value = "lng") Double lng,
                              @RequestParam(value = "lat") Double lat) {
        return UserDto.from(userService.updateUserLocation(id, lng, lat));
    }

}
